import * as React from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import {useState} from "react";
import axios from 'axios';

export default function CreateUser() {
    const [data, setData] = useState({
        name: "",
        login: "",
        email: ""
    })
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    function createUser(e) {
        e.preventDefault();
        axios.post('https://localhost:5001/api/users', {
            name: data.name,
            login: data.login,
            email: data.email
        })
        handleClose();
    }

    function handle(e) {
        const newdata = {...data}
        newdata[e.target.id] = e.target.value
        setData(newdata)
        console.log(newdata)
    }

    return (
        <div>
            <Button variant="outlined" onClick={handleClickOpen}>
                Создать пользователя
            </Button>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Создать пользователя</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="normal"
                        id="name"
                        label="Имя"
                        type="text"
                        size="normal"
                        variant="standard"
                        fullWidth
                        onChange={(e) => handle(e)}

                    />
                    <TextField
                        autoFocus
                        margin="normal"
                        id="login"
                        label="Логин"
                        type="text"
                        size="normal"
                        required
                        variant="standard"
                        fullWidth
                        onChange={(e) => handle(e)}
                    />
                    <TextField
                        autoFocus
                        margin="normal"
                        id="email"
                        label="Email Address"
                        type="email"
                        size="normal"
                        required
                        variant="standard"
                        fullWidth
                        onChange={(e) => handle(e)}

                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Отменить</Button>
                    <Button onClick={createUser}>Создать</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}
