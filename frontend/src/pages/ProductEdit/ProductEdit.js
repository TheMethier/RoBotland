import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import './ProductEdit.css';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

const ProductEdit = () => {
    const { id } = useParams();
    const [product, setProduct] = useState();
    const [editedProduct, setEditedProduct] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/products/Product/${id}`)
            .then(response => response.json())
            .then(data => {
                setProduct(data);
                setEditedProduct({
                    ...data, 
                });
            })
            .catch(error => console.log(error));
    }, [id]);


    const handleInputChange =( name, value ) => {
        setEditedProduct({
            ...editedProduct,
            [name]: value,
        });
    };

    const handleSaveClick = () => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/admin/products/Admin/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(editedProduct),
        })
        .then(response => response.json())
        .then(data => {
            console.log('Product updated successfully:', data);
            confirmAlert({
                title: 'Sukces',
                message: 'Produkt został pomyślnie zaktualizowany!',
                buttons: [
                    {
                        label: 'OK',
                        onClick: () => navigate(`/admin/productManagement`)
                    }
                ]
            });
            navigate(`/admin/productManagement`);
        })
        .catch(error => console.error('Error updating product:', error));
    };


    if (!product) {
        return <h1>Loading...</h1>
    }

    return ( 
        <div className="container">
            <div className="name">  
             <h2>{product.name}</h2>
            </div>
            <div className="content">           
                <div className="edit-form">
                    <form className="forms">
                        <label>Nazwa:</label>
                        <input
                            type="text"
                            name="name"
                            value={ editedProduct.name || product.name}
                            onChange={(e) => handleInputChange('name', e.target.value)}
                        />
                        <label>Opis:</label>
                        <textarea
                            name="description"
                            value={editedProduct.description || product.description}
                            onChange={(e) => handleInputChange('description', e.target.value)}
                        />
                        <label>Cena:</label>
                        <input
                            type="number"
                            name="price"
                            value={ editedProduct.price || product.price}  
                            onChange={(e) => handleInputChange('price', e.target.value)}
                        />
                        <label>Stan magazynowy:</label>
                        <input
                            type="number"
                            name="quantity"
                            value={ editedProduct.quantity || product.quantity}
                            onChange={(e) => handleInputChange('quantity', e.target.value)}
                        />
                        <label>Dostępność:</label>
                        <Select
                            className="select"
                            label="Dostępność"
                            name="isAvailable"
                            value={editedProduct.isAvailable || product.isAvailable}
                            onChange={(e) => handleInputChange('isAvailable', e.target.value)}
                        >
                            <MenuItem value={0}>0</MenuItem>
                            <MenuItem value={1}>1</MenuItem>
                            <MenuItem value={2}>2</MenuItem>
                        </Select>
                        <button type="button" onClick={handleSaveClick}>Zapisz</button>
                    </form>
                </div>
                <div className="edit-form">
                    <img src={`${process.env.REACT_APP_API_URL}/images/${product.imageUrl}`} alt={product.name} />
                </div>
            </div>
            
        </div>
    );
}
 
export default ProductEdit;