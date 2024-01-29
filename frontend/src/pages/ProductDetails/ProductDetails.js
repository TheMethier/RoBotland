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
               'credential':'include',
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
                        <p>{product.description}Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris mattis ex at turpis rhoncus, in volutpat lorem malesuada. Sed consequat venenatis est id aliquam. Vivamus non posuere quam. Nulla a bibendum tellus. Curabitur bibendum tortor sit amet massa pretium convallis. Quisque id cursus ligula, placerat posuere mi. Nunc viverra felis eu sem faucibus, sit amet sollicitudin purus rutrum. Vivamus sagittis nisi eu tortor bibendum facilisis.

Praesent rhoncus, sapien vitae sodales feugiat, risus nisl vulputate justo, id gravida nisi enim sit amet ipsum. Nunc laoreet convallis nibh, sed porta nulla efficitur vitae. Nulla id arcu at arcu scelerisque pellentesque ac ac metus. Mauris placerat ante sed risus ultrices scelerisque. Nunc nisl mi, aliquam rhoncus sem non, varius suscipit neque. Sed rutrum sollicitudin vulputate. Ut efficitur luctus dolor ut vulputate. Nunc consequat arcu dui, sit amet eleifend odio vulputate in.</p>
                    </div>
                    <div className="price">
                        <h2>{product.price} PLN</h2>
                        <p>
                        Dostępność: {product.isAvailable}

                        </p>
                        Stan magazynowy: {product.quantity}
                        <button className="addToCartBtn" onClick={(x)=>addProductToCard(product)}>Dodaj do koszyka</button>

                    </div>
                </div>
            </div>
        </div>
    );
}
 
export default ProductDetails;