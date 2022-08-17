
export const FileItem = (file) => {
  
  return (
    <div
    style={{
      margin: "5px",
      width: "30vw",
      border: "2px blue solid",
      wordWrap: "break-word"
    }}>
      <h6> {file.file.name}</h6>
      <h6> {file.file.relativePath}</h6>
    </div>
  )
}