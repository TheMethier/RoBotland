import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import './OrderDetails.css';


const OrderDetails = () => {
    const location = useLocation();
    const { state } = location;
    const order = state?.order;

    if (!order) {
        return <h1>Loading...</h1>
    }

    return ( 
        <div className="container">
            <div className="name">  
             <h1>{order.id}</h1>
             
       <p> {order.userDetails.firstName}</p>
    
            </div>
            {/* <div className="content">           
                <div className="img">
                    <img src={`${process.env.REACT_APP_API_URL}/images/${order.imageUrl}`} alt={order.name} />
                </div>
                <div className="info">
                    <div className="description">
                        <p>{order.description}</p>
                    </div>
                    <div className="price">
                        <h2>{order.price} PLN</h2>
                        Dostępność: {order.isAvailable}
                        <button className="addToCartBtn">Dodaj do koszyka</button>

                    </div>
                </div>
            </div> */}
        </div>
    );
}
 
export default OrderDetails;