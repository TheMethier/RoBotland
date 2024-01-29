import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card,  Button,  CardContent, Stack,Table } from '@mui/material';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';
import Grid from '@mui/material/Grid';
import RemoveShoppingCartIcon from '@mui/icons-material/RemoveShoppingCart';


export default function ShoppingCart()
{
  
    const [shoppingCartItems,setShoppingCartItems]= useState([]);
    const [press, setPress]=useState(false)
    const [total, setTotal]=useState(0);
    const navigate = useNavigate();

    useEffect(()=>{
      if(sessionStorage.getItem("cart")==null)setShoppingCartItems([]);
      setShoppingCartItems(JSON.parse(sessionStorage.getItem("cart")));

    },[press]);
    
    const addProductToCart= (product) =>{
      fetch(`${process.env.REACT_APP_API_URL}/api/v1/ShoppingCart/add/${product.id}`, {
          method: 'POST',
          headers: {
            'credential':'include',
            'Content-Type': 'application/json'
          },            
          body: sessionStorage.getItem("cart")
      })
      .then(response => {
          if(!response.ok)
              throw Error("");
          return response.json();
      })
      .catch(error => {
          console.log(error)
          
          return Promise.reject(error);
      })
      .then(data => {
          setShoppingCartItems(data);
          sessionStorage.setItem("cart",JSON.stringify(data));
          setPress(!press);
      });
  }
    const handleImageClick=(product)=>{
      navigate(`/products/${product.id}`);

    };
    const removeProductFromCart=(product)=>{
      fetch(`${process.env.REACT_APP_API_URL}/api/v1/ShoppingCart/remove/${product.id}`, {
        method: 'DELETE',
        headers: {
          'credential':'include',
          'Content-Type': 'application/json'
        },            
        body: sessionStorage.getItem("cart")
      })
      .then(response => {
        if(!response.ok)
            throw Error("");
        return response.json();
      })
      .catch(error => {
        return Promise.reject(error);
      })
      .then(data => {
        setShoppingCartItems(data);
        let total=0;
        shoppingCartItems.forEach((x)=>total+=x.total);
        setTotal(total)
        sessionStorage.setItem("cart",JSON.stringify(data));
        setPress(!press);
      });
    }

    const handlePlaceOrderClick=()=>{
      navigate("/placeorder")
    }
    function sum(acc, a)
    {
      return acc+a.total;
    }

    return(
      <div>
      {shoppingCartItems!=null && shoppingCartItems.length!=0 ? (
        <Stack sx={{display:"flex"}}>
            <TableContainer component={Paper} sx={{
              fontSize: "30px",
              marginTop: "2rem",
              width: "60rem",
              alignSelf: "center"}}>
              <Table  aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <TableCell>nr.</TableCell>
                    <TableCell align="center">Zdjęcie</TableCell>
                    <TableCell align="center">Nazwa</TableCell>
                    <TableCell align="center">Cena</TableCell>
                    <TableCell align="center">Ilość</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                {shoppingCartItems.map((row) => (
                  <TableRow
                    key={row.name}
                    >
                    <TableCell align="center">
                      <h1>
                        {row.id}
                      </h1>
                    </TableCell>
                    <TableCell align="center">
                      <img src={`${process.env.REACT_APP_API_URL}/images/${row.product.imageUrl}`}  alt={row.product.name} style={{ width: '200px' }}  />
                    </TableCell>
                    <TableCell align="center">
                      <h1>{row.product.name}</h1>
                    </TableCell>
                    <TableCell align="center">
                      <h1>{row.product.price} zł</h1>
                    </TableCell>
                    <TableCell align='center'>
                      <Button onClick={(x)=>addProductToCart(row.product)}disabled={row.quantity === row.product.quantity}>
                        <AddIcon />
                      </Button >
                      <h1>
                        {row.quantity}
                      </h1>
                      <Button onClick={(x)=>removeProductFromCart(row.product)}>
                        <RemoveIcon/>
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <Card sx={{alignSelf:"flex-end", marginRight:"23rem", marginTop:"1rem", height:"18rem", width:"21rem"}}>
 
            <CardContent>
              <h2>
              Podsumowanie zamówienia:
              </h2>
              <Grid container  columnSpacing={20} rowSpacing={0}>
                <Grid item xs={5} md={6}>
                <h2 >
                Razem:
                </h2>
                </Grid>
                <Grid item xs={5} md={6}>
                <h2 >
                  {shoppingCartItems.reduce((accumulate, total)=>accumulate+total.total,0)} zł
                </h2>
                </Grid>
      
              </Grid>
            
            <Button color="success" variant="contained" sx={{marginTop:"0rem", marginLeft:"0rem", alignSelf:"center", height:"4rem", width:"19rem"}} onClick={(t)=>{handlePlaceOrderClick()}}>
                    Złóż zamówienie
                  </Button> 
             
              </CardContent>
              </Card>
        </Stack>
      ) :(
        <Stack sx={{alignItems:"center"}}>
          <RemoveShoppingCartIcon sx={{height:"30rem", width:"30rem"}}/>         
          <h1 sx={{alignItems:"center"}}>
            Koszyk jest pusty !
          </h1>
        </Stack>
      )}
    </div>
  );
}