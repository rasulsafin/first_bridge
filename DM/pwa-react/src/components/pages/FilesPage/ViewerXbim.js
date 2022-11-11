import { Grid, LoaderOverlay, NavigationCube, Viewer, ViewType } from "@xbim/viewer";
import { useEffect, useRef, useState } from "react";
import { Controls } from "../../controls/Controls";
import { fileExtensions } from "../../../constants/fileExtensions";

export const ViewerXbim = () => {
  const [canvas, setCanvas] = useState();
  
  useEffect(() => {
    const elementCanvas = document.querySelector("#viewer");
    setCanvas(elementCanvas);
    console.log("effect");
  }, []);

  console.log(canvas);

  if (canvas !== undefined && canvas !== null) {
    const viewer = new Viewer(canvas);
    viewer.on('loaded', () => {
      viewer.show(ViewType.DEFAULT);
    });

    const cube = new NavigationCube();
    cube.ratio = 0.05;
    cube.passiveAlpha = cube.activeAlpha = 0.85;
    cube.minSize = 75;
    viewer.addPlugin(cube);

    const grid = new Grid();
    grid.zFactor = 20;
    grid.colour = [120, 120, 120, 0.8];
    viewer.addPlugin(grid);

    const loader = new LoaderOverlay();
    viewer.addPlugin(loader);

    let input = document.getElementById('input');
    input.addEventListener('change', () => {
      if (!input.files || input.files.length === 0) {
        return;
      }

      for (let i = 0; i < input.files.length; i++) {
        const file = input.files[i];
        viewer.load(file, file.name);
        viewer.start();
      }
      input.value = null;
    });
  }
  

//     function refreshModelsPanel() {
//
//
//       let html = <table>;
//       models.map((m) => {
//           (html += <tr>
//         html += <td>${m.name}</td>;
//         html += <td><button onclick='unload(" + m.id + ")'> Unload </button></td>;
//         if (m.stopped) {
//           html += <td> <button onclick='start(" + m.id + ")'> Start </button> </td>
//         } else {
//           html += <td> <button onclick='stopModel(" + m.id + ")'> Stop </button> </td>
//         }
//         html += </tr>)
//       });
//       html += </table>;
//       let modelsDiv = document.getElementById('models');
//       modelsDiv.innerHTML = html;
//     }
//     function unload(id: number) {
//       viewer.unload(id);
//       models = models.filter((m) => m.id !== id);
//       refreshModelsPanel();
//       viewer.draw();
//     }
//     function stop(id: number) {
//       viewer.stop(id);
//       const model = models.filter((m) => m.id === id).pop();
//       model.stopped = true;
//       refreshModelsPanel();
//     }
//
//     function start(id: number) {
//       viewer.start(id);
//       const model = models.filter((m) => m.id === id).pop();
//       model.stopped = false;
//       refreshModelsPanel();
//     }
//
//     window['unload'] = unload;
//     window['stopModel'] = stop;
//     window['start'] = start;
//
//     window['viewer'] = viewer;
// };
//
//   const handleClick = () => {
//     filePicker.current.click();
//   };
//
//   function handleChange() {
//     if (!input.files || input.files.length === 0) {
//       return;
//     }
//
//     for (let i = 0; i < input.files.length; i++) {
//       const file = input.files[i];
//       viewer.load(file, file.name);
//       viewer.start();
//     }
//
//     input.value = null;
//   }

  return (
      <div>
        Viewer Xbim
        <div>
          <div>
            {/*<Controls.Button*/}
            {/*  onClick={handleClick}*/}
            {/*> Pick File*/}
            {/*</Controls.Button>*/}
            {/*<input*/}
            {/*  className="hidden"*/}
            {/*  type="file"*/}
            {/*  ref={filePicker}*/}
            {/*  onChange={handleChange}*/}
            {/*  multiple="multiple"*/}
            {/*  accept=".wexbim"*/}
            {/*/>*/}
            <label htmlFor="input">Select wexbim files</label>
            <input  width="200" type="file" id="input" accept=".wexbim" multiple="multiple" />
          </div>
          <div id="models">
          </div>
        </div>
        <div id="fps-container">
          FPS: <span id="fps">60</span>
        </div>
        <canvas id="viewer" 
                width="1000" height="700"></canvas>
      </div>
    )
  }
