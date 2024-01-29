import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, FormControl, MenuItem, InputLabel, Select  ,TextField, Button,  CardContent, Stack,Table, CardHeader } from '@mui/material';
import { confirmAlert } from 'react-confirm-alert';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';





export default function PlaceOrder()
{
  
  const [user, setUser]= useState({});
  const [token, setToken]= useState("");
  const [isLoggedIn, setIsLoggedIn]= useState(false);
  const [orderDetails, setOrderDetails] = useState([]);
  const [order, setOrder]= useState({});
  const navigate = useNavigate();
  
  const handleLogin= () => {
    navigate("/login")  
  };
  const handleRegister= () => {
    navigate("/register")  
  
  };
  const handlePlaceOrder=()=>{
    navigate("/placeorderr")  
  
  };
  
  const handleChange=(event,name)=>{
      setOrder({...order,
      [name]:event.target.value});
      console.log(order);
  };
  useEffect(()=>{
      console.log(token)
      if(localStorage.getItem('token')!="" || localStorage.getItem('token')==null)
      {   
        setToken(localStorage.getItem('token'))
          setIsLoggedIn(true)
          setOrderDetails(JSON.parse(sessionStorage.getItem("cart")))
          fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/getUserInfo`,{
              method: 'GET',
              headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${token}` 
              }
          })
        .then((response)=>{
          if(!response.ok)
              throw Error("")
          return response.json()
      })
      .catch(error => {
          
          return ;
      })
          .then((data)=>{
              console.log(data)
              setUser(data);
        });
      }
      else
      {
          setIsLoggedIn(false)
      }
  },[token]);
  const handlePayClick= (order) => {
      console.log(orderDetails)
      const queryParams = new URLSearchParams(order).toString();
      console.log(`${process.env.REACT_APP_API_URL}/api/v1/Order/placeOrderByLoggedIn?${queryParams}`)
      fetch(`${process.env.REACT_APP_API_URL}/api/v1/Order/placeOrderByLoggedIn?${queryParams}`, {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${token}` 
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
          localStorage.setItem("token","");
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
        {isLoggedIn && user !=null ? (
          <div>
            <Grid container >
              <Grid item xs={5}>
                <Card
                  sx={{
                    borderRadius: '16px',  
                    position: 'absolute',
                    marginLeft: '25rem',
                    marginTop: "12rem",
                    transform: 'translate(-50%, -50%)',
                    width: "23rem",
                    height: "18rem", 
                    bgcolor: 'white',
                    boxShadow: 24,
                    p: 3, }}>
                  <CardContent>
                    <h1>
                      Dane osobiste
                    </h1>
                    <h2>
                     Imie i Nazwisko: {user.firstName} {user.lastName}
                    </h2>
                    <h2>
                     Email: {user.email} 
                    </h2>
                    <h2>
                     nr telefonu: +48 {user.phoneNumber}
                    </h2>
                  </CardContent>
                </Card>
              </Grid>
              <Grid item xs={4}>
                <Card sx={{
                  height:"30rem",
                  marginTop:"1rem",
                  alignContent: "center",
                  alignItems: 'center',
                  borderRadius: '16px',  
                  bgcolor: 'white',
                  boxShadow: 24,
                  minWidth:"50rem",
                  minHeight:"50rem",
                  }}>
                  <h1>
                    Podsumowanie zamówienia:
                  </h1>
                  <TableContainer component={Paper} sx={{
                    fontSize: "30px",
                    marginLeft:"1rem",
                    marginRight:"1rem",
                    minWidth:"30rem",
                    alignSelf: "center"}}>
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
                        <TableRow key={row.name}>
                          <TableCell align="center">
                          <img src={`${process.env.REACT_APP_API_URL}/images/${row.product.imageUrl}`}  alt={row.product.name} style={{ width: '200px' }}  />

                          </TableCell>
                          <TableCell align="center">
                            <p>{row.product.name}</p>
                          </TableCell>
                          <TableCell align='center'>
                            <h1>
                              {row.quantity}
                            </h1>
                          </TableCell>
                          <TableCell align="center">
                            <h1>{row.product.price} zł</h1>
                          </TableCell>
                        </TableRow>
                        ))}
                      </TableBody>
                    </Table>
                  </TableContainer>       
                  <h2>
                  Razem:                          {orderDetails.reduce((accumulate, total)=>accumulate+total.total,0)} zł
                  </h2>
                  <FormControl         sx={{
                    width: '18rem',
                    height: '4rem',
                    marginLeft:"12rem",
                    alignSelf:"center",
                    }}        
                  >
                    <InputLabel id="demo-simple-select-label">Rodzaj płatności</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={order.PaymentType}
                        label="PaymentType"
                        onChange={event =>handleChange(event,"PaymentType")}
                      >
                      <MenuItem value={0}>RoWallet (stan konta : {user.accountBalance} zł)</MenuItem>
                      <MenuItem value={1}>Płatność przy odbiorze</MenuItem>
                    </Select>
                  </FormControl>           
                  <div sx={{alignSelf: "center", marginTop:"40vh"}}>
                    <Button variant='contained' color='primary' onClick={(x)=>handlePayClick(order)} sx={{marginTop:"0rem", alignSelf:"center", height:"4rem", width:"19rem",marginLeft:"12rem"}}>
                      Zapłać
                    </Button>
                  </div>
                </Card>          
              </Grid>
              <Grid item xs={4}>
                <Card sx={{
                  borderRadius: '16px',  
                  marginLeft: '25rem',
                  transform: 'translate(-50%, -50%)',
                  height:"15rem", width:"24rem",
                  bgcolor: 'white',
                  boxShadow: 24,
                  p: 3, }}>
                  <h1>
                  Adres dostawy:
                  </h1>
                  <h3>
                  {user.street}
                  </h3>
                  <h3>
                  {user.zipCode} {user.city} 
                  </h3>
                  <FormControl sx={{
                    width: '18rem',
                    height: '4rem',
                    marginLeft:'4rem'
                  }}>
                    <InputLabel id="demo-simple-select-label">Rodzaj dostawy</InputLabel>
                    <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={order.DeliveryType}
                    label="DeliveryType"
                    onChange={event=>handleChange(event,"DeliveryType")}
                    >
                      <MenuItem value={0}>Paczkomat (20 zł)</MenuItem>
                      <MenuItem value={1}>Kurier InPost</MenuItem>
                      <MenuItem value={2}>Odbiór własny</MenuItem>
                    </Select>
                  </FormControl>
                </Card>          
              </Grid>
            </Grid>
          </div>)
        :(
          <>
            <Stack spacing={5} sx={{alignItems:"center"}}>
              <div>
                <h1>
                  Masz już konto?
                </h1>
                <Button variant='contained' color='primary' onClick={(x)=>handleLogin()}>
                  Zaloguj się
                </Button>
              </div>
              <div>
                <h1>
                  Nie masz konta?
                </h1>
                <Button variant='contained'color='success' onClick={(x)=>handleRegister()}>
                  Zarejestruj się
                </Button>
              </div>
              <div>
                <Button variant='contained' color='secondary' onClick={(x)=>handlePlaceOrder()}>
                  Kup bez rejestracji
                </Button>
              </div>
            </Stack>
          </>)}
        </div>
      );
    }
