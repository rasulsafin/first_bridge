import React, { useEffect, useState } from "react";
import {useLocation} from 'react-router-dom'
import Box from '@mui/material/Box'
import Drawer from '@mui/material/Drawer'
import {PropertiesPanel} from './SideDrawerPanel'


/**
 * SideDrawer contains the ItemPanel and CommentPanel and allows for
 * show/hide from the right of the screen.
 * it is connected to the global store and controlled by isDrawerOpen property.
 *
 * @return {object} SideDrawer react component
 */
export function SideDrawer({isDrawerOpen, closeDrawer, isPropertiesOn}) {

  useEffect(() => {
    if (!isPropertiesOn && isDrawerOpen) {
      closeDrawer()
    }
  }, [isPropertiesOn, isDrawerOpen, closeDrawer])

  return (
    <>
        <Drawer
          open={isDrawerOpen}
          anchor={'right'}
          variant='persistent'
          elevation={4}
          sx={{
            '&::-webkit-scrollbar': {
              display: 'none',
            },
            '& > .MuiPaper-root': {
              width: '400px',
              // This lets the h1 in ItemProperties use 1em padding but have
              // its mid-line align with the text in SearchBar
              padding: '4px 1em',
            },
            '& .MuiPaper-root': {
              marginTop: '0px',
              borderRadius: '0px',
            },
          }}
        >
          <Box sx={{
            height: '100%',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-between',
            overflowX: 'hidden',
            overflowY: 'auto',
          }}
          >
            <Box
              sx={{
                display: 'none',
                height: isPropertiesOn ? '50%' : '100%',
                borderRadius: '0px',
                borderBottom: `1px solid #212529`,
                paddingTop: '20px',
                overflowX: 'hidden',
                overflowY: 'auto',
              }}
            >
            </Box>
            <Box sx={{
              display: isPropertiesOn ? 'block' : 'none',
              height: '100%',
              borderRadius: '5px',
              overflowX: 'hidden',
              overflowY: 'auto',
            }}
            >
              {isPropertiesOn && <PropertiesPanel/>}
            </Box>
          </Box>
        </Drawer>
    </>
  )
}

export default function SideDrawerWrapper() {
  const [isDrawerOpen, setIsDrawerOpen] = useState(true)
  const [closeDrawer, setCloseDrawer] = useState(false)
  const [isPropertiesOn, setIsPropertiesOn] = useState(true)
  const [openDrawer, setOpenDrawer] = useState(true)

  return (
    <>
      {isDrawerOpen &&
        <SideDrawer
          isDrawerOpen={isDrawerOpen}
          closeDrawer={closeDrawer}
          isPropertiesOn={isPropertiesOn}
          openDrawer={openDrawer}
        />}
    </>
  )
}
