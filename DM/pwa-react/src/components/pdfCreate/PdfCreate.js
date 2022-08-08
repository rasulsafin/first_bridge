import { Document, Page, Text } from "@react-pdf/renderer";

export function PdfCreate() {

  const styles = StyleSheet.create({
    body: {
      paddingTop: 35,
      paddingBottom: 65,
      paddingHorizontal: 35,
    },
    title: {
      fontSize: 24,
      textAlign: "center",
    },
    text: {
      margin: 12,
      fontSize: 14,
      textAlign: "justify",
      fontFamily: "Times-Roman",
    },
    header: {
      fontSize: 12,
      marginBottom: 20,
      textAlign: "center",
      color: "grey",
    },
    pageNumber: {
      position: "absolute",
      fontSize: 12,
      bottom: 30,
      left: 0,
      right: 0,
      textAlign: "center",
      color: "grey",
    }
  })

  return (
    <Document>
      <Page style={styles.body}>
        <Text style={styles.header} fixed>
        </Text>
        <Text style={styles.text}></Text>
        <Text
        style={styles.pageNumber}
        render={({pageNumber, totalPages}) => `${pageNumber} / ${totalPages}`}
        fixed
        >
        </Text>
      </Page>
    </Document>
  );
}