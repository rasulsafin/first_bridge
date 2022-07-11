import * as React from 'react';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';

function Row(props) {

    const [open, setOpen] = React.useState(false);

    return (
        <React.Fragment>
            <TableRow sx={{'& > *': {borderBottom: 'unset'}}}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    > >
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {props.user.name}
                </TableCell>
                <TableCell align="right">{props.user.login}</TableCell>
                <TableCell align="right">{props.user.login}</TableCell>
                <TableCell align="right">{props.user.login}</TableCell>
                <TableCell align="right">{props.user.login}</TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{paddingBottom: 0, paddingTop: 0}} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{margin: 1}}>
                            <Typography variant="h6" gutterBottom component="div">
                                Project
                            </Typography>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Date</TableCell>
                                        <TableCell>Customer</TableCell>
                                        <TableCell align="right">Amount</TableCell>
                                        <TableCell align="right">Total price ($)</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        <TableRow>
                                            <TableCell component="th" scope="row">
                                                666
                                            </TableCell>
                                            <TableCell>666</TableCell>
                                            <TableCell align="right">666</TableCell>
                                            <TableCell align="right">
                                                666
                                            </TableCell>
                                        </TableRow>
                                    }
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

export default function CollapsibleTable(props) {

    return (
        <TableContainer component={Paper}
                        style={{border: "1px solid lightblue", margin: "20px", padding: "20px", width: "80%"}}>
            <Table aria-label="collapsible table">
                <TableHead>
                    <TableRow>
                        <TableCell/>
                        <TableCell>Имя</TableCell>
                        <TableCell align="right">email</TableCell>
                        <TableCell align="right">Логин</TableCell>
                        <TableCell align="right"> qwerty </TableCell>
                        <TableCell align="right"> qwerty </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {props.users.map(user => <Row user={user}/>)}

                </TableBody>
            </Table>
        </TableContainer>
    );
}
