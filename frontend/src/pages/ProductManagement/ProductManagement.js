import { DataGrid } from '@mui/x-data-grid';
import { Box, Button } from '@mui/material';
import './ProductManagement.css';
import Forms from '../ProductList/components/Forms';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css'; 

const ProductManagement = () => {
    const [products, setProducts] = useState([]);
    const [filter, setFilter] = useState({});
    const navigate = useNavigate();

    const fetchProducts= () => {
        const queryParams = new URLSearchParams(filter).toString();
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Product/filtred?${queryParams}`)
          .then((response) => response.json())
          .then((data) => setProducts(data))
          .catch((error) => console.log(error));
    };

    const handleRowClick = (row) => {
        navigate(`/products/${row.id}`);
    }

    const handleFilterChange = (newFilter) => {
        setFilter(newFilter);
    };

    useEffect(() => {
        fetchProducts();
    }, [filter]);

    const handleDeleteClick = (event, productId) => {
        event.stopPropagation();
        confirmAlert({
          title: 'Potwierdzenie',
          message: 'Czy na pewno chcesz usunąć ten produkt?',
          buttons: [
            {
              label: 'Tak',
              onClick: () => {
               
                  fetch(`${process.env.REACT_APP_API_URL}/api/v1/Product/${productId}`)
                      .then(response => response.json())
                      .then(data => {
                        const updatedProduct = { ...data, isAvailable: 3, quantity: 0 };
                        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Admin/products/${productId}`, {
                          method: 'PUT',
                          headers: {
                              'Content-Type': 'application/json',
                              'Authorization': `Bearer ${localStorage.getItem("token")}` 
                          },
                          body: JSON.stringify(updatedProduct),
                        })
                        .then(response => response.json())
                        .then(updatedData => {
                          console.log('Product deleted successfully:', updatedData);
                        })
                        .catch(error => console.error('Error updating product:', error));
                      })
                      .catch(error => console.error(error));
                    },
                  },
                  {
                    label: 'Nie',
                    onClick: () => {
                      console.log('Deletion canceled');
                    },
                  },
                ],
              });
            };

      const handleEditClick  = (event ,id) => {
        event.stopPropagation();
        console.log('Clicked Edit Button', id);
        navigate(`productEdit/${id}`);
      }
    
      function renderAvailability(params) {
        const availability = params.value;
      
        switch (availability) {
          case 0:
            return 'Wysyłka w 24h';
          case 1:
            return 'Wysyłka w 7 dni';
          case 2:
            return 'Niedostępny';
          default:
            return '';
        }
      }
      
      const columns = [
        { field: 'name', headerName: 'Nazwa',flex:1 ,style: { whiteSpace: 'normal', wordWrap: 'break-word' }, },
        { field: 'price', headerName: 'Cena', width: 75, renderCell: (params) => (`${params.value} PLN`) },
        { field: 'quantity', headerName: 'Stan magazynowy',width:200,style: {alignSelf: 'center'},renderCell: (params) => (
          <div>
            {params.value>5? <>{params.value} szt.</>:<div> Zamów nowe produkty lub zmień opcje dostępności!

                </div>}
          </div>
        ),width: 140,
        },
        {
          field: 'imageUrl', headerName: 'Zdjęcie', renderCell: (params) => (
            <img src={`${process.env.REACT_APP_API_URL}/images/${params.value}`} alt={params.row.name} style={{ height: 'auto' }} />
          ),
          width: 200, 
        },         
        { field: 'isAvailable', headerName: 'Dostępność', renderCell: renderAvailability ,width: 140 },
        {
            field: 'actions',
            headerName: 'Akcje',
            sortable: false,
            width: 100,
            renderCell: (params) => (
                <div className='buttons'>
                    <button className='button1' onClick={(event) => handleEditClick(event, params.row.id)}>Edytuj</button>
                    <button className='button1' onClick={(event) => handleDeleteClick(event, params.row.id)}>Usuń</button>
                </div>
            ),
            
        },
    ];

    return ( 
      <div><Box display="flex" >
            <div className='filtred_forms'>
              <Forms 
                onFilterChange={handleFilterChange}
                filter={filter}
              />
            </div>
            <div className='table'>
              {products.length > 0 ? (
                <DataGrid
                onRowClick={handleRowClick}
                rows={products}
                columns={columns}
                rowHeight={150} 
                />
                ) : (
                  <h1>Brak produktów</h1>
                )}
            </div>
      </Box></div>
    );
}
 
export default ProductManagement;