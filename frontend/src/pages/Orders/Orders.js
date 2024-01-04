import { DataGrid } from '@mui/x-data-grid';
import { Box } from '@mui/material';
import './Orders.css';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const columns = [
    { field: 'id', headerName: 'ID'},
    { field: 'createdDate', headerName: 'Data', width: 172 },
    {
        field: 'userDetails.FirstName',
        headerName: 'Użytkownik',
        flex: 1,
        valueGetter: (params) =>
            params.row.userDetails ?`${params.row.userDetails.firstName} ${params.row.userDetails.lastName}` : '',
    },
    {field: 'paymentType', headerName: 'Płatność', width: 70},
    { field: 'deliveryType', headerName: 'Dostawa' , width: 70},
    { field: 'orderStatus', headerName: 'Status' , width: 70},
];

const Orders = () => {

    const [orders, setOrders] = useState([]);
    const navigate = useNavigate();
    useEffect(() => {
    
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/admin/products/Admin/getOrders`)
          .then((response) => response.json())
          .then((data) => setOrders(data))
          .catch((error) => console.log(error));
    }, []);

    const handleRowClick = (params) => {
        const orderId = params.id;
        const selectedOrder = orders.find(order => order.id === orderId);
        navigate(`/admin/productManagement/orderDetails/${orderId}`, { state: { order: selectedOrder } });
    };
       
  
    return ( 
        <div><Box display="flex" >
            <div className='table'>
                {orders.length > 0 ? (
                    <DataGrid
                        onRowClick={(params) => handleRowClick(params.row)}
                        rows={orders}
                        columns={columns}
                        pageSizeOptions={[5, 10]}
                        rowHeight={150} 
                    />
                    ) : (
                        <h1>Brak zamówień</h1>
                    )}
            </div>
        </Box></div>
     );
}
 
export default Orders;