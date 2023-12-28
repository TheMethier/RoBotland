import { DataGrid } from '@mui/x-data-grid';
import './ProductList.css';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const columns = [
    { field: 'id', headerName: 'ID' },
    { field: 'name', headerName: 'Name' },
    { field: 'price', headerName: 'Price' },
];

const ProductList = () => {

    const [products, setProducts] = useState([]);

    const navigate = useNavigate();

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/products/Product/all`)
            .then(response => response.json())
            .then(data => setProducts(data))
            .catch(error => console.log(error));
    }, []);

    const handleRowClick = (row) => {
        navigate(`/products/${row.id}`);
    }


    return ( 
        <div className='table'>
            {products.length > 0 ? (
                <DataGrid
                    onRowClick={handleRowClick}
                    rows={products}
                    columns={columns}
                    pageSizeOptions={[5, 10]}
                />
                ) : (
                    <h1>Loading</h1>
                )}
        </div>
     );
}
 
export default ProductList;