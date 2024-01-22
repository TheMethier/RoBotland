import { DataGrid } from '@mui/x-data-grid';
import { Box } from '@mui/material';
import './ProductList.css';
import Forms from './components/Forms.js';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const columns = [
    { field: 'id', headerName: 'ID'},
    { field: 'name', headerName: 'Nazwa',flex: 1, },
    { field: 'price', headerName: 'Cena'},
    {
        field: 'imageUrl', headerName: 'Zdjęcie', renderCell: (params) => (
          <img src={`${process.env.REACT_APP_API_URL}/images/${params.value}`} alt={params.row.name} style={{ height: 'auto' }} />
        ),
        width: 200, 
      },
      
    { field: 'isAvailable', headerName: 'Dostępność' },
];

const ProductList = () => {

    const [products, setProducts] = useState([]);
    const [filter, setFilter] = useState({}); 

    const navigate = useNavigate();

    useEffect(() => {

        const queryParams = new URLSearchParams(filter).toString();
    
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Product/filtred?${queryParams}`)
          .then((response) => response.json())
          .then((data) => setProducts(data))
          .catch((error) => console.log(error));
    }, [filter]);

    const handleRowClick = (row) => {
        navigate(`/products/${row.id}`);
    }

    const handleFilterChange = (newFilter) => {
        setFilter(newFilter);
    };
   
    
     
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
                        pageSizeOptions={[5, 10]}
                        rowHeight={150} 
                    />
                    ) : (
                        <h1>Brak produktów</h1>
                    )}
            </div>
        </Box></div>
     );
}
 
export default ProductList;