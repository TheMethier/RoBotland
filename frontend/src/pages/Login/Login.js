
import React, { useState } from 'react';
import {  useNavigate } from 'react-router-dom';
import { Card, TextField, Paper, Button,  Stack, CardContent } from '@mui/material';
import { confirmAlert } from 'react-confirm-alert';
import {useForm} from 'react-hook-form'
import { Password } from '@mui/icons-material';
export default function Login()
{

    const navigate = useNavigate();
    const defaultLogin={
        username: "",
        password: ""
      };
    const [errors, setErrors]=useState([]);
    const [login, setLogin] =useState(defaultLogin);
    const handleLoginChange=(name, value)=>{
        setLogin({...login,
        [name]:value});
    };
    const handleRegisterClick= () => {
        navigate("/register");
    }
    const handleLoginClick = () => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(login)
        })
        .then(response => {
            if(!response.ok)
                {
                    response.text().then(text => {
                        if(text!="Not Found"&& text!="Bad Password"){
                            console.log(text);
                            let p=JSON.parse(text);
                            setErrors(p.errors);
                            console.log(JSON.stringify(p.errors))
                        }
                        else{
                            console.log(text);
                            if(text=="Not Found"){
                                let error={Username:["Bad Username","",""]};
                                setErrors(error);
                            }
                            else 
                            {
                                let error={Password:["Bad Password","",""]};
                                setErrors(error);                     
                            }
                            

                        }
                    });
                }
                else{
                    return response.json();

                }
        })

        .then(data => {
            if(data!=null)
            {
                localStorage.setItem("token",data.token);
                confirmAlert({
                    title: 'Sukces',
                    message: 'Zalogowano!',
                    buttons: [
                        {
                            label: 'OK',
                            onClick: () => window.location.replace("/")
                        }
                    ]
                });
            }
                        
        })        .catch(error => {
        });
    };
    return(
        <div>
            <Card
             sx={{
                marginTop: "13rem",
                borderRadius: '16px',  
                position: 'absolute',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                width: "23rem",
                height: "16rem",
                bgcolor: 'white',
                boxShadow: 24,
                p: 4, }}>
                    <CardContent>
            <Stack  
                spacing={1}   
                alignItems="center"   
                justifyContent="center"
                sx={{}}>
            <div>

                <TextField
                    id="outlined-username-input-standard-size-normal"
                    label="Username"
                    type='username'
                    autoComplete='current-username'
                    size="normal"
                    sx={{
                        width: '20rem',
                        height: '4rem',
                        marginBottom: errors?.["Username"]?
                        "1rem": null,
                    }}
                    error={!!errors?.["Username"]}       
                    helperText={errors?.["Username"]? errors["Username"][0]: null}
                    onChange={(x)=>handleLoginChange("username",x.target.value)}
                    
                />
                  </div>
                  <div>
                <TextField
                    id="outlined-password-input-standard-size-normal"
                    label="Password"
                    type="password"
                    autoComplete="current-password"
                    size="normal"
                    sx={{
                        width: '20rem',
                        height: '4rem',
                        marginBottom: errors?.["Password"]?
                        "1rem": null,    
                    }}
                    onChange={(x)=>handleLoginChange("password",x.target.value)}
                    error={!!errors?.["Password"]}
                    helperText={errors?.["Password"]? errors["Password"][0]: null}
                />
                </div>
                <div>
                <Button variant="contained"
                    size="large"
                    sx={{
                        width: '20rem',
                        height: '4rem',
                        fontFamily: 'sans-serif',
                        fontWeight: "border"
                    }}
                    onClick={x=>{handleLoginClick()}}
                    >
                    Zaloguj się
                </Button>
                </div>
            </Stack>
            </CardContent>
            </Card>
            <Paper sx={{ marginTop: "40rem",
                borderRadius: '16px',  
                position: 'absolute',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                width: "25rem",
                height: "8rem",
                bgcolor: 'white',
                boxShadow: 24,
                p: 2, }}>
                    <Stack  
                spacing={1}   
                alignItems="center"   
                justifyContent="center"
                sx={{}}>

            <div>
                    <h2>Pierwszy raz w Robotlandzie? </h2>
                </div>
                <div>
                <Button variant="contained"
                color='success'
                    size="large"
                    sx={{
                        width: '12rem',
                        height: '3rem',
                        fontFamily: 'sans-serif',
                        fontWeight: "border"
                    }}
                    onClick={()=>{handleRegisterClick();}}
                    >
                    Zarejestruj się
                </Button>
                </div>
                </Stack>

            </Paper>
        </div>
    );
}