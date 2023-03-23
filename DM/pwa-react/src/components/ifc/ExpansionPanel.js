import React, {useEffect, useState} from 'react'
import Box from '@mui/material/Box'
import Accordion from '@mui/material/Accordion'
import AccordionSummary from '@mui/material/AccordionSummary'
import AccordionDetails from '@mui/material/AccordionDetails'
import Typography from '@mui/material/Typography'
import { ReactComponent as CaretIcon } from "../../assets/icons/caret.svg";

export default function ExpansionPanel({summary, detail, expandState}) {
  const [expanded, setExpanded] = useState(expandState)

  useEffect(() => {
    setExpanded(expandState)
  }, [expandState])


  return (
    <Accordion
      elevation={0}
      sx={{
        '& .MuiAccordionSummary-root': {
          width: '100%',
          padding: 0,
          borderBottom: `.5px solid gray`,
        },
        '& .MuiAccordionSummary-root.Mui-expanded': {
          marginBottom: '0.5em',
        },
        '& .MuiAccordionDetails-root': {
          padding: 0,
        },
        '& svg': {
          width: '14px',
          height: '14px',
          marginRight: '12px',
          marginLeft: '12px',
        },
      }}
      expanded={expanded}
      onChange={() => setExpanded(!expanded)}
    >
      <AccordionSummary
        expandIcon={<CaretIcon/>}
        aria-controls="panel1a-content"
        id="panel1a-header"
      >
        <Typography sx={{
          'maxWidth': '320px',
          'whiteSpace': 'nowrap',
          'overflow': 'hidden',
          'textOverflow': 'ellipsis',
          '@media (max-width: 900px)': {
            maxWidth: '320px',
          },
        }}
        >
          <Box>{summary}</Box>
        </Typography>
      </AccordionSummary>
      <AccordionDetails>
        {detail}
      </AccordionDetails>
    </Accordion>
  )
}
