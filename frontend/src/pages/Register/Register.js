import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, TextField, Button,  CardContent } from '@mui/material';
import Grid from '@mui/material/Grid';
import { confirmAlert } from 'react-confirm-alert';
export default function Register()
{
    const [errors, setErrors]=useState([]);
    const navigate = useNavigate();
    const defaultRegister={
         username: "",
         password: "",
         firstName: "",
         lastName: "",
         email: "",
         phoneNumber: "",
         street: "",
         city: "",
         houseNumber: "",
         zipCode: ""
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
                    response.text().then(text => {
                        console.log(text);
                        let p=JSON.parse(text);
                        setErrors(p.errors);
                        console.log(p.errors)
                    });
                }
                else{
                    return response.json();
                }
        })
        .catch(error => {
            console.log(error)
        })
        .then(data => {
            if(data)
            {
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
        }            
        });
    }
    return(

<Card
             sx={{
                marginTop: "23rem",
                borderRadius: '16px',  
                position: 'absolute',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                width: "23rem",
                height: "42rem",
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
                        height: '4rem',
                        marginBottom: errors?.["Username"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("username",x.target.value)}
                    error={!!errors?.["Username"]}       
                    helperText={errors?.["Username"]? errors["Username"][0]: null}

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
                        height: '4rem',
                        marginBottom: errors?.["Password"]?
                        "0.5rem": null
    
                    }}
                    onChange={(x)=>handleChange("password",x.target.value)}
                    error={!!errors?.["Password"]}       
                    helperText={errors?.["Password"]? errors["Password"][0]: null}

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
                        height: '4rem',
                        marginBottom: errors?.["Email"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("email",x.target.value)}
                    error={!!errors?.["Email"]}       
                    helperText={errors?.["Email"]? errors["Email"][0]: null}

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
                        height: '4rem',
                        marginBottom: errors?.["PhoneNumber"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("phoneNumber",x.target.value)}
                    error={!!errors?.["PhoneNumber"]}       
                    helperText={errors?.["PhoneNumber"]? errors["PhoneNumber"][0]: null}
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
                        height: '4rem',
                        marginBottom: errors?.["FirstName"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("firstName",x.target.value)}
                    error={!!errors?.["FirstName"]}       
                    helperText={errors?.["FirstName"]? errors["FirstName"][0]: null}
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
                        height: '4rem',
                        marginBottom: errors?.["LastName"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("lastName",x.target.value)}
                    error={!!errors?.["LastName"]}       
                    helperText={errors?.["LastName"]? errors["LastName"][0]: null}
                />  
        </Grid>
        <Grid item xs={10}md={7}>
        <TextField
                    id="outlined-zipCode-input-standard-size-normal"
                    label="City" 
                    type='text'
                    autoComplete='current-city'
                    size="normal"
                    sx={{
                        height: '4rem',
                        marginBottom: errors?.["City"]?
                        "2rem": null
                    }}
                    onChange={(x)=>handleChange("City",x.target.value)}
                    error={!!errors?.["City"]}       
                    helperText={errors?.["City"]? errors["City"][0]: null}
                />  
        </Grid>       
        <Grid item xs={10}md={5}>
        <TextField
                    id="outlined-zipCode-input-standard-size-normal"
                    label="zipCode" 
                    type='text'
                    autoComplete='current-zipCode'
                    size="normal"
                    sx={{
                        height: '4rem',
                        marginBottom: errors?.["ZipCode"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("zipCode",x.target.value)}
                    error={!!errors?.["ZipCode"]}       
                    helperText={errors?.["ZipCode"]? errors["ZipCode"][0]: null}
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
                        height: '4rem',
                        marginBottom: errors?.["Street"]?
                        "0.5rem": null
                    }}
                    onChange={(x)=>handleChange("street",x.target.value)}
                    error={!!errors?.["Street"]}       
                    helperText={errors?.["Street"]? errors["Street"][0]: null}
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
                        height: '4rem',
                        marginBottom: errors?.["HouseNumber"]?
                        "2rem": null
                    }}
                    onChange={(x)=>handleChange("houseNumber",x.target.value)}
                    error={!!errors?.["HouseNumber"]}       
                    helperText={errors?.["HouseNumber"]? errors["HouseNumber"][0]: null}
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