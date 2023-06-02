import React from 'react';
import { Drawer as MuiDrawer } from '@mui/material';

export const Drawer = (props) => {
  return (
    <MuiDrawer
      sx={{
        display: 'flex',
        flexDirection: 'column',
        '& .MuiPaper-root': {
          minWidth: '415px',
        },
      }}
      anchor="right"
      {...props}
    />
  );
};
