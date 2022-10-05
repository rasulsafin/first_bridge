export function ErrorFallback({error, resetErrorBoundary}) {
  return (
    <div className="m-3 mt-5 pt-5" role="alert">
      <p><h3>WTF?!?! Something went wrong:</h3></p>
      <img alt="errorpic" src="/errorpic.jpg" style={{
        width: 250,
        borderRadius: 10
      }}/>
      <div style={{
        width: 450
      }}>
        {error.message}
      </div>   
      <button onClick={resetErrorBoundary}>Try again</button>
    </div>
  )
}