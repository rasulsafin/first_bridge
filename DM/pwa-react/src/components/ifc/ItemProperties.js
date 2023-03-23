import React, {useEffect, useState} from 'react'
import Box from '@mui/material/Box'
import Typography from '@mui/material/Typography'
// import {createPropertyTable} from '../../utils/itemProperties'
import { Switch } from "@mui/material";
import ExpansionPanel from "./ExpansionPanel";
import { useSelector } from "react-redux";
import { selectElement, selectIfcModel } from "../../services/ifcModelSlice";
import { createPropertyTable } from "./utils/itemProperties";


function decodeIFCString(ifcString) {
  const ifcUnicodeRegEx = /\\X2\\(.*?)\\X0\\/uig
  let resultString = ifcString
  let match = ifcUnicodeRegEx.exec(ifcString)
  while (match) {
    const unicodeChar = String.fromCharCode(parseInt(match[1], 16))
    resultString = resultString.replace(match[0], unicodeChar)
    match = ifcUnicodeRegEx.exec(ifcString)
  }
  return resultString
}

export default function ItemProperties() {
  const [propTable, setPropTable] = useState(null)
  const [psetsList, setPsetsList] = useState(null)
  const [expandAll, setExpandAll] = useState(false)
  const model = useSelector(selectIfcModel);
  const element = useSelector(selectElement);

  useEffect(() => {
    (async () => {
      if (model && element) {
        setPropTable(await createPropertyTable(model, element))
        setPsetsList(await createPsetsList(model, element, expandAll))
      }
    })()
  }, [model, element, expandAll])

  return (
    <Box sx={{
      '& td': {
        minWidth: '130px',
        maxWidth: '130px',
        verticalAlign: 'top',
        cursor: 'pointer',
        padding: '3px 0',
        borderBottom: `.2px solid grey`,
      },
      '& table': {
        tableLayout: 'fixed',
        width: '100%',
        overflow: 'hidden',
        borderSpacing: 0,
      },
      '& .MuiSwitch-root': {
        float: 'right',
      },
      '& .MuiSwitch-track': {
        backgroundColor: "grey",
        opacity: 0.8,
        border: 'solid 2px grey',
      },
      '& .MuiSwitch-thumb': {
        backgroundColor: "black",
      },
    }}
    >
      {/*{propTable}*/}
      {psetsList && psetsList.props.children.length > 0 &&
        <Box sx={{
          marginTop: '10px',
        }}
        >
          <Typography
            sx={{
            position: 'sticky',
            top: '0px',
            display: 'flex',
            flexDirection: 'row',
            alignItems: 'center',
            justifyContent: 'space-between',
            background: "#B3B3B3",
            zIndex: 1000,
          }}
          >
            Property Sets
            <Switch
              checked={expandAll}
              onChange={() => setExpandAll(!expandAll)}
            />
          </Typography>
          {psetsList}
        </Box>
      }
    </Box>
  )
}

async function createPsetsList(model, element, expandAll) {
  const psets = await model.getPropertySets(element.expressID)
  return (
    <Box component='ul' sx={{
      margin: 0,
      height: '100%',
      width: '100%',
      padding: '0px 0px 50px 0px',
    }}
    >
      {await Promise.all(
        psets.map(
          async (ps, ndx) => {
            return (
              <ExpansionPanel
                key={`pset-${ndx}`}
                summary={decodeIFCString(ps.Name.value) || 'Property Set'}
                detail={await createPropertyTable(model, ps, true, 0)}
                expandState={expandAll}
              />
            )
          },
        ))}
    </Box>
  )
}
