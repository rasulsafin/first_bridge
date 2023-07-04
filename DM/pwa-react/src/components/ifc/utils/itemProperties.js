import React, { useState } from "react";
import Typography from "@mui/material/Typography";
import * as IfcTypesMap from "./IfcTypesMap";

function isTypeValue(obj) {
  if (obj === null || obj === undefined) {
    return false;
  }
  const is = obj["type"] != null && obj["value"] != null;
  return is;
}

function stoi(s) {
  const i = parseInt(s);
  if (!isFinite(i)) {
    throw new Error(`Expected integer, got: ${s}`);
  }
  return i;
}

function decodeIFCString(ifcString) {
  const ifcUnicodeRegEx = /\\X2\\(.*?)\\X0\\/uig;
  let resultString = ifcString;
  let match = ifcUnicodeRegEx.exec(ifcString);

  while (match) {
    const unicodeChar = String.fromCharCode(parseInt(match[1], 16));
    resultString = resultString.replace(match[0], unicodeChar);
    match = ifcUnicodeRegEx.exec(ifcString);
  }
  return resultString;
}

async function deref(ref, webIfc = null, indent = "") {
  if (ref === null || ref === undefined) {
    return "null";
  }
  if (Array.isArray(ref)) {
    // Dereference array values.
    await (async () => {
      for (let i = 0; i < ref.length; i++) {
        ref[i] = await deref(ref[i], webIfc, indent + "  ");
      }
    })();
    return ref;
  } else if (typeof ref === "object") { // must be after array check
    if (isTypeValue(ref)) {
      switch (ref.type) {
        case 1:
          return decodeIFCString(ref.value); // string
        case 2:
          return ref.value;
        case 3:
          return ref.value;
        case 4:
          return ref.value;
        case 5: {
          const refId = stoi(ref.value);
          const refElt = await deref(await webIfc.properties.getItemProperties(0, refId, true), webIfc);
          // not recursing deref on global elt
          if (refElt.GlobalId) {
            return {
              type: refElt.type,
              ref: refElt.GlobalId
            };
          }
          return refElt;
        }
        default:
          throw new Error("Unknown reference type: " + ref);
      }
    } else {
      for (const objKey in ref) {
        if (!Object.prototype.hasOwnProperty.call(ref, objKey)) {
          continue;
        }
        const val = ref[objKey];
        // TODO: https://technical.buildingsmart.org/resources/ifcimplementationguidance/ifc-guid/
        // if (objKey == 'GlobalId' && ref.expressID) {
        //   const guid = webIfc.ifcGuidMap.get(parseInt(ref.expressID))
        //   console.error(`#${ref.expressID} GlobalId: `, val, guid)
        // }
        if (objKey === "type") {
          ref[objKey] = IfcTypesMap.getName(val, true);
        } else if (objKey === "GlobalId" && val.type === 1) {
          ref[objKey] = val.value;
        } else {
          ref[objKey] = await deref(val, webIfc, indent + "  ");
        }
      }
      return ref;
    }
  }
  return ref; // number or string, e.g. the value of Name or expressID
}

export async function createPropertyTable(model, ifcProps, isPset = false, serial = 0) {
  const ROWS = [];
  let rowKey = 0;

  if (ifcProps.constructor &&
    ifcProps.constructor.name &&
    ifcProps.constructor.name !== "IfcPropertySet") {
    ROWS.push(<Row d1={"IFC Type"} d2={ifcProps.constructor.name} key={`type-${serial}`} />);
  }

  for (const key in ifcProps) {
    if (isPset && (key === "expressID" || key === "Name")) {
      continue;
    }

    const val = ifcProps[key];
    const propRow = await prettyProps(model, key, val, false, rowKey++);

    if (propRow) {
      if (propRow.key === null) {
        throw new Error(`Row for key=(${key}) created with invalid react key`);
      }
      ROWS.push(propRow);
    }
  }

  return (
    <table key={`table-${serial++}`}>
      <tbody>{ROWS}</tbody>
    </table>
  );
}

async function prettyProps(model, propName, propValue, isPset, serial = 0) {
  let label = `${propName}`;
  const refPrefix = "Ref";
  if (label.startsWith(refPrefix)) {
    label = label.substring(refPrefix.length);
  }
  if (propValue === null || propValue === undefined || propValue === "") {
    return null;
  }
  switch (propName) {
    case "type":
    case "CompositionType":
    case "GlobalId":
    case "ObjectPlacement":
    case "ObjectType":
    case "OwnerHistory":
    case "PredefinedType":
    case "Representation":
    case "RepresentationContexts":
    case "Representations":
    case "Tag":
    case "UnitsInContext":
      return null;
    case "Coordinates":
    case "RefLatitude":
    case "RefLongitude":
      return (
        <Row
          d1={label}
          d2={
            dms(
              await deref(propValue[0]),
              await deref(propValue[1]),
              await deref(propValue[2]))
          }
          key={serial}
        />
      );
    case "expressID":
      return <Row d1={"Express Id"} d2={propValue} key={serial} />;
    case "Quantities":
      return await quantities(model, propValue, serial);
    case "HasProperties":
      return await hasProperties(model, propValue, serial);
    default: {
      if (propValue.type === 0) {
        return null;
      }
      return (
        <Row
          d1={label}
          d2={
            await deref(
              propValue, model, serial,
              async (v, mdl, srl) => await createPropertyTable(mdl, v, srl))
          }
          key={serial}
        />
      );
    }
  }
}

export async function quantities(model, quantitiesObj, serial) {
  return await unpackHelper(model, quantitiesObj, serial, (ifcElt, rows) => {
    const name = decodeIFCString(ifcElt.Name.value);
    let val = "value";
    for (const key in ifcElt) {
      if (key.endsWith("Value")) {
        val = ifcElt[key].value;
        break;
      }
    }
    val = decodeIFCString(val);
    rows.push(<Row d1={name} d2={val} key={serial++} />);
  });
}

export async function unpackHelper(model, eltArr, serial, ifcToRowCb) {
  if (Array.isArray(eltArr)) {
    const rows = [];

    for (const i in eltArr) {
      if (Object.prototype.hasOwnProperty.call(eltArr, i)) {
        const p = eltArr[i];
        const refTypeVal = 5;
        if (p.type !== refTypeVal) {
          throw new Error("Array contains non-reference type");
        }
        const refId = stoi(p.value);
        if (model.getItemProperties) {
          const ifcElt = await model.getItemProperties(refId);
          ifcToRowCb(ifcElt, rows);
        } else {
        }
      }
    }
    return (
      <tr key={`hasProps-${serial++}`}>
        <td colSpan={2}>
          <table>
            <tbody>{rows}</tbody>
          </table>
        </td>
      </tr>
    );
  }
  return null;
}

export async function hasProperties(model, hasPropertiesArr, serial) {
  if (!Array.isArray(hasPropertiesArr)) {
    throw new Error("hasPropertiesArr should be array");
  }
  return await unpackHelper(model, hasPropertiesArr, serial, (dObj, rows) => {
    const name = decodeIFCString(dObj.Name.value);
    const value = (dObj.NominalValue === undefined || dObj.NominalValue === null) ?
      "<error>" :
      decodeIFCString(dObj.NominalValue.value);
    rows.push(<Row d1={name} d2={value} key={serial++} />);
  });
}

function Row({ d1, d2 }) {
  const [isActive, setIsActive] = useState(false);
  const toggleActive = () => {
    setIsActive(!isActive);
  };
  const rowStyleInactive = {
    whiteSpace: "nowrap",
    overflow: "hidden",
    textOverflow: "ellipsis"
  };
  if (d1 === null || d1 === undefined || d2 === undefined) {
  }
  if (d2 === null) {
    return <tr onDoubleClick={toggleActive}>
      <td colSpan="2">{d1}</td>
    </tr>;
  }

  return (
    isActive ? (
      <tr onDoubleClick={toggleActive}>
        <td colSpan={2}>
          <Typography variant="propTitle" sx={{ display: "block" }}>{d1}</Typography>
          <Typography variant="propValue">{d2}</Typography>
        </td>
      </tr>
    ) : (
      <tr onDoubleClick={toggleActive}>
        <td style={rowStyleInactive}><Typography variant="propTitle">{d1}</Typography></td>
        <td style={rowStyleInactive}><Typography variant="propValue">{d2}</Typography></td>
      </tr>
    )
  );
}

export function reifyName(webIfc, element) {
  if (element.LongName) {
    if (element.LongName.value) {
      return decodeIFCString(element.LongName.value.trim())
    }
  } else if (element.Name) {
    if (element.Name.value) {
      return decodeIFCString(element.Name.value.trim())
    }
  }
  return prettyType(webIfc, element) + ''
}

const dms = (deg, min, sec) => {
  return `${deg}° ${min}' ${sec}''`;
};

export function getType(webIfc, elt) {
  console.log(webIfc.properties)
  console.log(elt)
  console.log(webIfc.properties.getIfcType(elt.type))
  return webIfc.properties.getIfcType(elt.type)
}

export function prettyType(webIfc, elt) {
  switch (getType(webIfc, elt)) {
    case 'IFCANNOTATION': return 'Note'
    case 'IFCBEAM': return 'Beam'
    case 'IFCBUILDING': return 'Building'
    case 'IFCBUILDINGSTOREY': return 'Storey'
    case 'IFCBUILDINGELEMENTPROXY': return 'Element (generic proxy)'
    case 'IFCCOLUMN': return 'Column'
    case 'IFCCOVERING': return 'Covering'
    case 'IFCDOOR': return 'Door'
    case 'IFCFLOWSEGMENT': return 'Flow Segment'
    case 'IFCFLOWTERMINAL': return 'Flow Terminal'
    case 'IFCPROJECT': return 'Project'
    case 'IFCRAILING': return 'Railing'
    case 'IFCROOF': return 'Roof'
    case 'IFCSITE': return 'Site'
    case 'IFCSLAB': return 'Slab'
    case 'IFCSPACE': return 'Space'
    case 'IFCWALL': return 'Wall'
    case 'IFCWALLSTANDARDCASE': return 'Wall (std. case)'
    case 'IFCWINDOW': return 'Window'
    default:
      return elt.type
  }
}

export function groupElementsByTypes(element, elementTypes) {
  const type = prettyType(element.type)
  if (elementTypes === undefined) {
    elementTypes = []
  }
  const lookup = elementTypes.filter((t) => t.name === type)
  if (lookup.length === 0) {
    elementTypes.push({
      name: type,
      elements: [{expressID: element.expressID,
        Name: element.Name,
        LongName: element.LongName}],
    })
  } else {
    lookup[0].elements.push({expressID: element.expressID,
      Name: element.Name,
      LongName: element.LongName})
  }
  if (element.children.length > 0) {
    element.children.forEach((e) => {
      groupElementsByTypes(e, elementTypes)
    })
  }

  return elementTypes
}
