import React, { useEffect, useState } from 'react';
import { useNavigate, useParams, useLocation } from 'react-router-dom';
import { Card,Drawer, List,ListItem,ListItemButton, ListItemIcon,ListItemText, TextField, Button,  CardContent, Stack,Table, CardHeader, Typography } from '@mui/material';
import { confirmAlert } from 'react-confirm-alert';
import ReceiptLongIcon from '@mui/icons-material/ReceiptLong';
import { DataGrid } from '@mui/x-data-grid';
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
import AccountBalanceWalletIcon from '@mui/icons-material/AccountBalanceWallet';

function UserPanel()
{   
    const [user, setUser]= useState({
        accountBalance: 0,
        username: "string",
        password: "",
        firstName: "string",
        lastName: "string",
        email: "user@example.com",
        phoneNumber: "111 222 333",
        street: "a",
        city: "b",
        houseNumber: "c",
        zipCode: "d"
      });
      const columns = [
        { field: 'id', headerName: 'ID', width: 300},
        { field: 'createdDate', headerName: 'Data złożenia zamówienia',width: 250, 
    },
        { field: 'deliveryType', headerName: 'Rodzaj dostawy', renderCell: (params) => (
            <>
                {params==="0"?(<Typography variant='body1'>Odbiór własny</Typography>):(<Typography variant='body1'>InPost</Typography>)}
            </>), width: 200},
        { field: 'paymentType', headerName: 'Rodzaj płatności', renderCell: (params) => (
            <>
                {params==="0"?(<Typography variant='body1'>RoWallet</Typography>):(<Typography variant='body1'>Płatność przy odbiorze</Typography>)}
            </>
        ),
        width: 200,  },
        {field:'orderStatus',headerName: 'Status zamówienia', renderCell: (params) => (
            <>
                {params==="0"?(<Typography variant='body1'>Wysłana</Typography>):(<Typography variant='body1'>W trakcie realizacji</Typography>)}
            </>),width:200},
        {
            field: 'items', headerName: 'Szczegóły zamówienia', renderCell: (params) => (
                <>
              <ReceiptLongIcon sx={{width:"10rem",height: "10rem" }}/>      
                </>
            ),
            width: 200, 
          },
          
        
    ];
    const [userHistory, setUserHistory]= useState([])
    const [cash, setCash]= useState({});

    useEffect(()=>{
        if(localStorage.getItem('token')!=="" || localStorage.getItem('token')===null)
        {   
            fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/getUserInfo`,{
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}` 
                }
            })
          .then((response)=>{
            if(!response.ok)
                throw Error("")
            return response.json()
        })
        .catch(error => {
            console.log(error);
            return ;
        })
            .then((data)=>{
                console.log(data)
                setUser(data);
          });
          fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/getHistory`,{
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}` 
            }
        })
      .then((response)=>{
        if(!response.ok)
            throw Error("")
        return response.json()
    })
    .catch(error => {
        console.log(error);
        return ;
    })
        .then((data)=>{
            console.log(data)
            setUserHistory(data);
      });

    }
    
    },[localStorage.getItem('token')]);      
    
    const handleChange=(event)=>{
        setCash({...cash,
            ["amount"]:event.target.value});
        console.log(cash);
    };
    const handleDeposit=()=>{
        const queryParams = new URLSearchParams(cash).toString();
        console.log(localStorage.getItem('token'));
        console.log(`${process.env.REACT_APP_API_URL}/api/v1/User/depositToAccount?${queryParams}`)

        fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/depositToAccount?${queryParams}`,{
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `bearer ${localStorage.getItem('token')}` 
            }
        })
      .then((response)=>{
        if(!response.ok)
            throw Error("")
        return response.json()
    })
    .catch(error => {
        console.log(error);
        return ;
    })
        .then((data)=>{
            confirmAlert({
                title: 'Sukces',
                message: 'Dodano środki!',
                buttons: [
                    {
                        label: 'OK',
                        onClick: () => window.location.replace(`/user/${user.username}`)
                    }
                ]
            });
        });
    }


    return(
        <div>
            <Grid>
                <Grid item>
                    <Card >
                        <CardHeader title="Szczegóły użytkownika"></CardHeader>
                        <CardContent>
                            <Typography variant="body1" gutterBottom>
                                email : {user.email}
                            </Typography>
                            <Typography variant="body1" gutterBottom>
                                nr.telefonu: +48 {user.phoneNumber} 
                            </Typography>
                            <Typography variant="body1" gutterBottom>
                                 Imie:  {user.firstName}
                            </Typography>
                            <Typography variant="body1" gutterBottom>
                                Nazwisko: {user.lastName}
                            </Typography>
                            <Typography variant="body1" gutterBottom>
                                Adres zamieszkania: {user.street} {user.houseNumber} {user.zipCode}{user.city}
                            </Typography>                        
                            </CardContent>
                    </Card>
                </Grid>
                <Grid item>
                <div>

            <div className='table'>
                {userHistory.length > 0 ? (
                    <DataGrid
                        rows={userHistory}
                        columns={columns}
                        pageSizeOptions={[5, 10]}
                        rowHeight={150} 
                        sx={{width:"90rem"}}
                    />
                    ) : (
                        <h1>Brak produktów</h1>
                    )}
            </div>
       </div>

                </Grid>
                <Grid item>
                <Card >
                        <CardHeader title="Stan mojego RoWallet"></CardHeader>
                        <CardContent>

                           {user.accountBalance} zł
                           
                           <TextField
                          id="outlined-firstname-input-standard-size-normal"
                          label="Kwota"
                          type='lastname'
                          autoComplete='current-lastname'
                          size="normal"
                          sx={{
                              height: '4rem'
                          }}
                          onChange={(x)=>handleChange(x)}
                        />  
                           <Button onClick={(x)=>handleDeposit()}>
                           Doładuj portfel
                           </Button>

                            </CardContent>
                    </Card>
                    
                </Grid>
            </Grid>
        </div>
    );
}
export default UserPanel;
