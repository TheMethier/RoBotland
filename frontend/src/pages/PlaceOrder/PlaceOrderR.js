import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, TextField, Button,  CardContent,FormControl,Table, MenuItem, InputLabel, Select  } from '@mui/material';
import Grid from '@mui/material/Grid';
import { confirmAlert } from 'react-confirm-alert';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';


export default function PlaceOrderR()
{
    const [order, setOrder]= useState({});
    const [orderDetails, setOrderDetails]= useState([]);
    useEffect(()=>
    {
      setOrderDetails(JSON.parse(sessionStorage.getItem("cart")));
    },[]);
    const handleChange=(name, value)=>
    {
      setOrder({...order,
      [name]:value});
    };
    const handlePayClick= (order) => 
    {
      const queryParams = new URLSearchParams(order).toString();
      console.log(`${process.env.REACT_APP_API_URL}/api/v1/Order/placeOrderWithoutRegister?${queryParams}`)
      fetch(`${process.env.REACT_APP_API_URL}/api/v1/Order/placeOrderWithoutRegister?${queryParams}`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(orderDetails)
      })
      .then(response => {
          if(!response.ok)
              {
                  throw Error("");
              }
          return response.json();
      })
      .catch(error => {
          console.log(error)
          return Promise.reject(error);
      })
      .then(data => {
          sessionStorage.setItem("cart","[]");
          confirmAlert({
              title: 'Sukces',
              message: 'Złożono zamówienie!',
              buttons: [
                  {
                      label: 'OK',
                      onClick: () => window.location.replace("/")
                  }
              ]
          });
          
      });
    
    
              
    
    }
    return(
       <div>
          <Grid container rowSpacing={1}>
            <Grid item xs={5}>
              <Card
                sx={{
                marginTop: "12rem",
                marginLeft:"18rem",
                borderRadius: '16px',  
                position: 'absolute',
                transform: 'translate(-50%, -50%)',
                width: "23rem",
                height: "18rem",
                bgcolor: 'white',
                boxShadow: 24,
                p: 4, }}>
                  <CardContent>
                    <h1>
                      Dane osobiste
                    </h1>
                    <Grid container spacing={1}>
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
                          onChange={(x)=>handleChange("Email",x.target.value)}
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
                          onChange={(x)=>handleChange("PhoneNumber",x.target.value)}
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
                          onChange={(x)=>handleChange("FirstName",x.target.value)}
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
                          onChange={(x)=>handleChange("LastName",x.target.value)}
                        />  
                      </Grid>
                    </Grid>
                  </CardContent>
                </Card>
              </Grid>
              <Grid item xs={4}>
                <Card sx={{
                  marginTop: "23rem",
                  marginLeft:"32rem",
                  borderRadius: '16px',  
                  position: 'absolute',
                  maxHeight:"100rem",
                  minWidth:"30rem",
                  maxWidth:"70rem",
                  left: '50%',
                  transform: 'translate(-50%, -50%)',
                  bgcolor: 'white',
                  boxShadow: 24,
                  p: 4, }}>
                    <h1>
                      Podsumowanie zamówienia:
                    </h1>
                   <TableContainer component={Paper} sx={{
                      fontSize: "30px",
                      marginTop: "2rem",
                      maxWidth: "70rem",
                      alignSelf: "center",
                      }}>
                      <Table  aria-label="simple table">
                        <TableHead>
                          <TableRow>
                            <TableCell align="center">Zdjęcie</TableCell>
                            <TableCell align="center">Nazwa</TableCell>
                            <TableCell align="center">Ilość</TableCell>
                            <TableCell align="center"> Cena
                            </TableCell>
                          </TableRow>
                        </TableHead>
                        <TableBody>
                          {orderDetails.map((row) => (
                          <TableRow
                            key={row.name}>   
                              <TableCell align="center">
                                <img alt={row.product.name}sx={{width:"10rem", height : "10rem"}} />
                              </TableCell>
                              <TableCell align="center">
                                <h1>{row.product.name}</h1>
                              </TableCell>
                              <TableCell align='center'>
                                <h1>
                                  {row.quantity}
                                </h1>
                              </TableCell>
                              <TableCell align="center">
                                <h1>{row.product.price} zł</h1>
                              </TableCell>
                          </TableRow>))}
                        </TableBody>
                      </Table>
                    </TableContainer>        
                    <h2>
                      Razem:                          {orderDetails.reduce((accumulate, total)=>accumulate+total.total,0)} zł
                    </h2>
                    <FormControl>
                      <InputLabel id="demo-simple-select-label">Rodzaj płatności</InputLabel>
                      <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={order.PaymentType}
                        label="DeliveryType"
                        onChange={x=>handleChange("PaymentType",x.target.value)}
                        sx={{
                            width: '21rem',
                            height: '4rem'}}>
                        <MenuItem value={0}>Blik</MenuItem>
                        <MenuItem value={1}>Przy odbiorze</MenuItem>
                      </Select>
                    </FormControl>
                    <div sx={{alignSelf: "center", marginTop:"40vh"}}>
                      <Button  
                        variant='contained'
                        color='primary'
                        onClick={(x)=>handlePayClick(order)} 
                        sx={{
                          marginTop:"0rem",
                          alignSelf:"center",
                          height:"4rem",
                          width:"19rem",
                          marginLeft:"6rem"}}>
                          Zapłać
                        </Button>
                    </div>
                  </Card>
                </Grid>
                <Grid item xs={5}>
                  <Card
                  sx={{
                    marginLeft: "18rem",
                    marginTop: "35rem",
                    borderRadius: '16px',  
                    position: 'absolute',
                    transform: 'translate(-50%, -50%)',
                    width: "23rem",
                    height: "18rem",
                    bgcolor: 'white',
                    boxShadow: 24,
                    p: 4, }}>
                    <CardContent>
                      <h1>
                                Dane do dostawy
                      </h1>
                      <Grid container spacing={1}>
                        <Grid item xs={10} md={6}>
                          <TextField
                            id="outlined-zipCode-input-standard-size-normal"
                            label="City" 
                            type='text'
                            autoComplete='current-zipCode'
                            size="normal"
                            sx={{
                                height: '4rem'
                            }}
                            onChange={(x)=>handleChange("City",x.target.value)}
                          />  
                        </Grid>                
                        <Grid item xs={10}md={6}>
                          <TextField
                              id="outlined-zipCode-input-standard-size-normal"
                              label="zipCode" 
                              type='text'
                              autoComplete='current-zipCode'
                              size="normal"
                              sx={{
                                  height: '4rem',
                              }}
                              onChange={(x)=>handleChange("ZipCode",x.target.value)}
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
                            onChange={(x)=>handleChange("Street",x.target.value)}
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
                            onChange={(x)=>handleChange("HouseNumber",x.target.value)}
                            />  
                        </Grid>
                        <Grid item xs={10}>
                          <FormControl>
                              <InputLabel id="demo-simple-select-label">Rodzaj dostawy</InputLabel>
                              <Select
                                labelId="demo-simple-select-label"
                                id="demo-simple-select"
                                value={order.DeliveryType}
                                label="DeliveryType"
                                onChange={x=>handleChange("DeliveryType",x.target.value)}
                                sx={{
                                width: '21rem',
                                height: '4rem'}}>
                                <MenuItem value={0}>Paczkomat (20 zł)</MenuItem>
                                <MenuItem value={1}>Kurier InPost</MenuItem>
                                <MenuItem value={2}>Odbiór własny</MenuItem>
                              </Select>
                            </FormControl>
                        </Grid>
                      </Grid>
                    </CardContent>
                  </Card>
                </Grid>
              </Grid>
            </div>

    );
}
