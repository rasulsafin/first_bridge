export function ErrorFallback({ error, resetErrorBoundary }) {
  return (
    <div role="alert">
      <p><h3>WTF?!?! Something went wrong:</h3></p>
      <img alt="errorpic" src="/errorpic.jpg" style={{
        width: 250,
        borderRadius: 10
      }} />
      <div style={{
        width: 450
      }}>
        {error.message}
      </div>
      <button onClick={resetErrorBoundary}>Try again</button>
    </div>
  );
}