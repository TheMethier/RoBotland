import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import './ProductDetails.css';


const ProductDetails = () => {
    const { id } = useParams();


    const [product, setProduct] = useState();

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/products/Product/${id}`)
            .then(response => response.json())
            .then(data => setProduct(data))
            .catch(error => console.log(error));
    }, [id]);

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
                        <button className="addToCartBtn">Dodaj do koszyka</button>

                    </div>
                </div>
            </div>
        </div>
    );
}
 
export default ProductDetails;