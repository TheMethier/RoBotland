import React, { useEffect, useState } from 'react';
import { useNavigate, useParams, useLocation } from 'react-router-dom';
import { Card,Paper,TextField, Button,  CardContent, Stack,Table, CardHeader, Typography } from '@mui/material';
import { confirmAlert } from 'react-confirm-alert';
import ReceiptLongIcon from '@mui/icons-material/ReceiptLong';
import { DataGrid } from '@mui/x-data-grid';
import Grid from '@mui/material/Grid';
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
function UserPanel()
{   
    const [open, setOpen]= useState(false);
    const [order, setOrder]=useState([]);
    const handleOpen = (params) =>{ 
        setOpen(true);
        console.log(params.row.items);
        setOrder(params.row.items);
    };
    const handleClose = () => setOpen(false);  
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
              <ReceiptLongIcon sx={{width:"10rem",height: "10rem" }} onClick={x=>handleOpen(params)}/>      
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
            if(data){

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
        }
        });
    }


    return(
        <div>

            <Grid container rowSpacing={2} columnSpacing={2}>
                <Grid item  md={8}>
                    <Card sx={{minHeight:"20rem",maxHeight:'100rem',minWidth:"20rem" ,maxWidth:"20rem",marginLeft:'4rem'}}>
                        <CardHeader title="Szczegóły użytkownika:"></CardHeader>
                        <CardContent>
                            <Typography variant="h6" gutterBottom>
                                Email : {user.email}
                            </Typography>
                            <Typography variant="h6" gutterBottom>
                                Nr.telefonu: +48 {user.phoneNumber} 
                            </Typography>
                            <Typography variant="h6" gutterBottom>
                                 Imie:  {user.firstName}
                            </Typography>
                            <Typography variant="h6" gutterBottom>
                                Nazwisko: {user.lastName}
                            </Typography>
                            <Typography variant="h6" gutterBottom>
                                Adres zamieszkania: {user.street} {user.houseNumber} {user.zipCode}{user.city}
                            </Typography>                        
                            </CardContent>
                    </Card>
                </Grid>
                <Grid item  >
                <Card sx={{maxHeight:'40rem', maxWidth:"20rem"}} >
                        <CardHeader title="Stan mojego RoWallet:"></CardHeader>
                        <CardContent>
                        <h1>
                            {user.accountBalance} zł

                        </h1>   
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
            <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={{position: 'absolute',
            top: '50%',
            left: '50%',
            transform: 'translate(-50%, -50%)',
            bgcolor: 'background.paper',
            border: '2px solid #000',
            boxShadow: 24,
            p: 4}}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
                Szczegóły zamówienia
          </Typography>
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
                {order!=null?(order.map((row) => (
                  <TableRow
                    key={row.product.name}
                    >
                    <TableCell align="center">
                      <h1>
                        {row.id}
                      </h1>
                    </TableCell>
                    <TableCell align="center">
                      <img alt={row.product.name} sx={{width:"10rem", height : "10rem"}}/>
                    </TableCell>
                    <TableCell align="center">
                      <h1>{row.product.name}</h1>
                    </TableCell>
                    <TableCell align="center">
                      <h1>{row.product.price} zł</h1>
                    </TableCell>
                    <TableCell align="center">
                      <h1>{row.quantity}</h1>
                    </TableCell>
                  </TableRow>
                  
                ))):(<div></div>)}
              </TableBody>
            </Table>
          </TableContainer>
          <h2>
                  Razem:    {order.reduce((accumulate, total)=>accumulate+total.total,0)} zł
        </h2>
        </Box>
      </Modal>
       </div>

                </Grid>
                
            </Grid>
        </div>
    );
}
export default UserPanel;
