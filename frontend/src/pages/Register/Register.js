import React, { useState } from 'react';
import { json, useNavigate } from 'react-router-dom';
import { Card, TextField, Paper, Button, Typography, Stack, CardContent, CardHeader } from '@mui/material';
import Grid from '@mui/material/Grid';

import { confirmAlert } from 'react-confirm-alert';
export default function Register()
{
    const navigate = useNavigate();
    const defaultRegister={
         username: "string",
         password: "string",
         firstName: "string",
         lastName: "string",
         email: "user@example.com",
         phoneNumber: "string",
         street: "string",
         city: "string",
         houseNumber: "string",
         zipCode: "string"
      }
    const [register, setRegister] =useState(defaultRegister);
    const handleChange=(name, value)=>{
        setRegister({...register,
        [name]:value});
    };
    const handleRegisterClick= () => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(register)
        })
        .then(response => {
            if(!response.ok)
                {
                    console.log(register);
                    console.log(response);
                    throw Error("");
                }
            return response.json();
        })
        .catch(error => {
            console.log(error)
            return Promise.reject(error);
        })
        .then(data => {
            confirmAlert({
                title: 'Sukces',
                message: 'Zarejestrowano!',
                buttons: [
                    {
                        label: 'OK',
                        onClick: () => window.location.replace("/login")
                    }
                ]
            });
            
        });
    }
    return(

<Card
             sx={{
                marginTop: "20rem",
                borderRadius: '16px',  
                position: 'absolute',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                width: "23rem",
                height: "35rem",
                bgcolor: 'white',
                boxShadow: 24,
                p: 4, }}>
                    <CardContent>
      <Grid container spacing={1}>
      <Grid item xs={10}>
      <TextField
                    id="outlined-username-input-standard-size-normal"
                    label="Username"
                    type='username'
                    autoComplete='current-username'
                    size="normal"
                    sx={{
                        width: '21rem',
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("username",x.target.value)}
                />
        </Grid>
        <Grid item xs={10}>
        <TextField
                    id="outlined-password-input-standard-size-normal"
                    label="Password"
                    type="password"
                    autoComplete="current-password"
                    size="normal"
                    sx={{
                        width: '21rem',
                        height: '4rem'
    
                    }}
                    onChange={(x)=>handleChange("password",x.target.value)}
                />
        </Grid>
        <Grid item xs={10}>
        <TextField
                    id="outlined-email-input-standard-size-normal"
                    label="Email"
                    type='email'
                    autoComplete='current-email'
                    size="normal"
                    sx={{
                        width: '21rem',
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("email",x.target.value)}
                />  
        </Grid>
        <Grid item xs={10}>

        <TextField
                    id="outlined-phonenumber-input-standard-size-normal"
                    label="PhoneNumber"
                    type='tel'
                    autoComplete='current-phonenumber'
                    size="normal"
                    sx={{
                        width: '21rem',
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("phoneNumber",x.target.value)}
                />  
        </Grid>
        <Grid item xs={10} md={6}>
        <TextField
                    id="outlined-firstname-input-standard-size-normal"
                    label="Firstname"
                    type='firstname'
                    autoComplete='current-firstname'
                    size="normal"
                    sx={{
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("firstName",x.target.value)}
                />  
        </Grid>
        <Grid item xs={10} md={6}>
        <TextField
                    id="outlined-firstname-input-standard-size-normal"
                    label="Lastname"
                    type='lastname'
                    autoComplete='current-lastname'
                    size="normal"
                    sx={{
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("lastName",x.target.value)}
                />  
        </Grid>
        <Grid item xs={10}>
        <TextField
                    id="outlined-zipCode-input-standard-size-normal"
                    label="zipCode + city" 
                    type='text'
                    autoComplete='current-zipCode'
                    size="normal"
                    sx={{
                        width: '21rem',
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("zipCode",x.target.value)}
                />  
        </Grid>       

        <Grid item xs={10} md={7}>
          
        <TextField
                    id="outlined-street-input-standard-size-normal"
                    label="Street"
                    type='text'
                    autoComplete='current-street'
                    size="normal"
                    sx={{
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("street",x.target.value)}
                />  

        </Grid>
        <Grid item xs={10} md={5}>

        <TextField
                    id="outlined-houseNumber-input-standard-size-normal"
                    label="HouseNumber"
                    type='text'
                    autoComplete='current-houseNumber'
                    size="normal"
                    sx={{
                        height: '4rem'
                    }}
                    onChange={(x)=>handleChange("houseNumber",x.target.value)}
                />  

        </Grid>
        
        <Grid item xs={10}>
        <Button variant="contained"
                    size="large"
                    sx={{
                        width: '21rem',
                        height: '4rem',
                        fontFamily: 'sans-serif',
                        fontWeight: "border"
                    }}
                    onClick={()=>{handleRegisterClick();}}
                    >
                    Zarejestruj się
                </Button>
        </Grid>
      </Grid>
      </CardContent>
      </Card>
);
}