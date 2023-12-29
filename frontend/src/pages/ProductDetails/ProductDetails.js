import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './ProductDetails.css';


const ProductDetails = () => {
    const { id } = useParams();

    const navigate = useNavigate();

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
            <h1>{product.name}</h1>
            <div className="up">           
                <div className="img">
                    <img src={`${process.env.REACT_APP_API_URL}/images/${product.imageUrl}`} alt={product.name} />
                </div>
                <div className="info">
                    <h2>{product.price} PLN</h2>
                    <p>{product.description}</p>
                </div>
            </div>
        </div>

    
    );
}
 
export default ProductDetails;