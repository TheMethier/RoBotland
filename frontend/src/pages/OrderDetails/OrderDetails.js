import { useLocation } from "react-router-dom";
import './OrderDetails.css';
import { DataGrid } from '@mui/x-data-grid';
import { useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';


const columns = [
    { field: 'id', headerName: 'ID'},
    { field: 'productName', headerName: 'Nazwa', flex: 1 },
    {
        field: 'img', headerName: 'Zdjęcie', renderCell: (params) => (
          <img src={`${process.env.REACT_APP_API_URL}/images/${params.value}`} alt={params.row.productName} style={{ height: 'auto' }} />
        ),
        width: 200, 
      },
    { field: 'productPrice', headerName: 'Cena', flex: 1 },
    { field: 'quantity', headerName: 'Sztuki'},
    { field: 'total', headerName: 'Kwota' },

];

const OrderDetails = () => {
    const location = useLocation();
    const { state } = location;
    const order = state?.order;
    const navigate = useNavigate();

    const flattenOrderItems = order.items.map(item => ({
        id: item.id,
        productId: item.product.id,
        productName: item.product.name,
        productPrice: item.product.price,
        quantity: item.quantity,
        total: item.total,
        img: item.product.imageUrl,
    }));

    const handleRowClick = (row) => {
        navigate(`/products/${row.row.productId}`);
    }

    const handleChangeStatusClick = async () => {
        let selectedStatus = '';
    
        await new Promise((resolve) => {
          confirmAlert({
            title: 'Status zamówienia',
            message: 'Na jaki status chcesz zmienić?',
            buttons: [
              {
                label: 'Wysłane',
                onClick: () => {
                  selectedStatus = 0;
                  resolve();
                },
              },
              {
                label: 'W trakcie realizacji',
                onClick: () => {
                  selectedStatus = 1;
                  resolve();
                },
              },
            ],
          });
        });
        console.log('Selected Status:', selectedStatus);

  if (selectedStatus) {
    fetch(`${process.env.REACT_APP_API_URL}/api/v1/Admin/changeOrderStatus/${order.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem("token")}` 

      },
      body: JSON.stringify( selectedStatus ),
    })
      .then(response => response.json())
      .then(data => {
        console.log('Status zamówienia został zmieniony:', data);
      })
      .catch(error => {
        console.error('Błąd podczas zmiany statusu zamówienia:', error);
      });
  }
    };

    if (!order) {
        return <h1>Loading...</h1>
    }

    return ( 
        <div className="container">
            <div className="name">  
       <p> {order.userDetails.firstName} {order.userDetails.lastName}</p>
            </div>
        <div className="table_button">
            <div className='table'>
                {order.items.length > 0 ? (
                    <DataGrid
                        onRowClick={handleRowClick}
                        rows={flattenOrderItems}
                        columns={columns}
                        pageSizeOptions={[5, 10]}
                        rowHeight={150} 
                    />
                    ) : (
                        <h1>Brak produktów</h1>
                    )}
            </div>
            <div>
                <button onClick={handleChangeStatusClick} className="status-button">
                    Zmień status zamówienia
                </button>
            </div>
            </div>
        </div>
    );
}
 
export default OrderDetails;