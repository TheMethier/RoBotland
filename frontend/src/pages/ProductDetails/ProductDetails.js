import { useEffect, useState } from "react";
import { json, useParams } from "react-router-dom";
import './ProductDetails.css';
import { confirmAlert } from 'react-confirm-alert';


const ProductDetails = () => {
    const { id } = useParams();
    const [cart, setCart]= useState([]);

    const [product, setProduct] = useState();

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/Product/${id}`)
            .then(response => response.json())
            .then(data => setProduct(data))
            .catch(error => console.log(error));
    }, [id]);
    
    const addProductToCard= (product) =>{
        let cart=sessionStorage.getItem("cart")
        if (cart==null){
            sessionStorage.setItem("cart","[]")
            setCart([])
        }
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/ShoppingCart/add/${product.id}`, {
            method: 'POST',
            headers: {
                    'Content-Type': 'application/json'
                    
            },            
            body: sessionStorage.getItem("cart")
        })
        .then(response => {
            if(!response.ok)
                {
                    console.log(product);
                    console.log(response);
                    throw Error("");
                }
            return response.json();
        })
        .catch(error => {
            console.log(error)
            return Promise.reject(error);
        })
        .then(data => {
            console.log(data);
            sessionStorage.setItem("cart",JSON.stringify(data));
            confirmAlert({
                title: 'Sukces',
                message: `Dodano ${product.name} do koszyka!`,
                buttons: [
                    {
                        label: 'OK',
                        onClick: () => window.location.replace("/cart")
                    }
                ]
            });
            
        });
    }
    if (!product) {
        return <h1>Loading...</h1>
    }

    return ( 
        <div className="container">
            <div className="name">  
             <h1>{product.name}</h1>
            </div>
            <div className="content">           
                <div className="img">
                    <img src={`${process.env.REACT_APP_API_URL}/images/${product.imageUrl}`} alt={product.name} />
                </div>
                <div className="info">
                    <div className="description">
                        <p>{product.description}</p>
                    </div>
                    <div className="price">
                        <h2>{product.price} PLN</h2>
                        Dostępność: {product.isAvailable}
                        <button className="addToCartBtn" onClick={(x)=>addProductToCard(product)}>Dodaj do koszyka</button>

                    </div>
                </div>
            </div>
        </div>
    );
}
 
export default ProductDetails;