import React from "react";

export const ModelCard = (file) => {
  return (
    <div
      style={{
        width: "179px",
        height: "183px",
        backgroundColor: "lightgray",
        display: "flex",
        alignItems: "end"
      }}>
      <span>{file.file.name}</span>
    </div>
  );
};
