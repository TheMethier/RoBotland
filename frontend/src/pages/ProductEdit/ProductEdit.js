import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import './ProductEdit.css';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { useNavigate } from 'react-router-dom';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';
import Checkbox from '@mui/material/Checkbox';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

const ProductEdit = () => {
    const { id } = useParams();
    const [product, setProduct] = useState();
    const [editedProduct, setEditedProduct] = useState({});
    const navigate = useNavigate();
    const [categories, setCategories] = useState([]);
    const [productCategories, setProductCategories] = useState([]);
    const [newCategory, setNewCategory] = useState('');
    const [selectedCategories, setSelectedCategories] = useState([]);

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Product/${id}`)
            .then(response => response.json())
            .then(data => {
                setProduct(data);
                setEditedProduct({
                    ...data, 
                });
            })
            .catch(error => console.log(error));
    }, [id]);

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Admin/categories/${id}`,{            
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
    })
        .then(response => response.json())
        .then(data => {
            setProductCategories(data);
            setSelectedCategories(data.map(category => category.name));

        })
        .catch(error => console.log(error));
    }, [id]);

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Product/categories`)
            .then(response => response.json())
            .then(data => setCategories(data))
            .catch(error => console.error('Error fetching categories:', error));
    }, []);

    const handleInputChange =( name, value ) => {
        setEditedProduct({
            ...editedProduct,
            [name]: value,
        });
    };

    const handleSaveClick = () => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Admin/products/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}` 

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
            const productId = data;
            if (selectedCategories.length > 0) {
                fetch(`${process.env.REACT_APP_API_URL}/api/v1/Admin/categories/products`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.getItem("token")}` 

                    },
                    body: JSON.stringify({ CategoryNames: selectedCategories, ProductId: productId }),
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Error adding category to product');
                    }
                    return response.json();
                })
                .then(categoryData => {
                    console.log('Response kghjggjjfhghory to product:', categoryData);
                })
                .catch(error => {
                    console.error('Error adding category to product:', error);
                });
            }
            navigate(`/admin/productManagement`);
        })
        .catch(error => console.error('Error updating product:', error));
    };

    const handleCategoryChange = (categoryName, checked) => {
        if (checked) {
            setSelectedCategories([...selectedCategories, categoryName]);
        } else {
            setSelectedCategories(selectedCategories.filter(id => id !== categoryName));
        }
    };

    const handleNewCategoryChange = (e) => {
        setNewCategory(e.target.value);
    };

    const handleAddNewCategory = () => {
        if (newCategory.trim() === '') {
            return;
        }
        if (categories.some(category => category.name === newCategory)) {
            return;
        }
        
        const newCategoryObject = {name: newCategory };
        
        setCategories([...categories, newCategoryObject]);
        setNewCategory('');
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
                        <button className="button" type="button" onClick={handleSaveClick}>Zapisz</button>
                    </form>
                </div>
                <div className="edit-form">
                        <form className="forms">
                        <div className="checkboxContainer">
                            <label>Kategorie:</label>
                            <div className="categoryContainer">
                                <div className="categoryColumn">
                                    {categories.slice(0, Math.ceil(categories.length / 2)).map(category => (
                                    <div key={category.id}>
                                        <Checkbox
                                        checked={selectedCategories.includes(category.name)}
                                        onChange={(e) => handleCategoryChange(category.name, e.target.checked)}
                                        />
                                        {category.name}
                                    </div>
                                    ))}
                                </div>
                                <div className="categoryColumn">
                                    {categories.slice(Math.ceil(categories.length / 2)).map(category => (
                                    <div key={category.id}>
                                        <Checkbox
                                        checked={selectedCategories.includes(category.name)}
                                        onChange={(e) => handleCategoryChange(category.name, e.target.checked)}
                                        />
                                        {category.name}
                                    </div>
                                    ))}
                                </div>
                                </div>
                    <div className="butoo"> 
                        <TextField
                            label="Nowa kategoria"
                            value={newCategory}
                            onChange={handleNewCategoryChange}
                        />
                        <Button variant="contained" onClick={handleAddNewCategory}>
                            Dodaj nową kategorię
                        </Button></div>
                        
                    
                   </div>
                </form>
            </div>
            </div>
        </div>
    );
}
 
export default ProductEdit;