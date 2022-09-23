export function ErrorFallback({error, resetErrorBoundary}) {
  return (
    <div role="alert">
      <p><h3>Something went wrong:</h3></p>
      <pre>{error.message}</pre>
      <button onClick={resetErrorBoundary}>Try again</button>
    </div>
  )
}