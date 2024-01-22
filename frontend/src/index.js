import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Home from './pages/Home/Home';
import UserPanel from './pages/UserPanel/UserPanel';
import Navbar from './components/Navbar/Navbar';
import ProductList from './pages/ProductList/ProductList';
import ProductDetails from './pages/ProductDetails/ProductDetails';
import ProductManagement from './pages/ProductManagement/ProductManagement';
import ProductEdit from './pages/ProductEdit/ProductEdit';
import AddNewProduct from './pages/AddNewProduct/AddNewProduct';
import OrderDetails from './pages/OrderDetails/OrderDetails';
import Orders from './pages/Orders/Orders';
import Admin from './components/Admin/Admin';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import ShoppingCart from './pages/ShoppingCart/ShoppingCart';
import PlaceOrder from './pages/PlaceOrder/PlaceOrder' 
import PlaceOrderR from './pages/PlaceOrder/PlaceOrderR';
//TODO
//tabelka z produktami do dodania?
//dodawanie zdjeć?
//dostęp do panelu admina 
//obsługa zamówień
const router = createBrowserRouter(
  [
    {
      path: '/',
      element: <Navbar />,
      children: [
        {
          path: '/',
          element: <Home />,
        },
        {
          path: '/products',
          element: <ProductList />
        },
        {
          path: '/user/:user',
          element: <UserPanel  />
        }
        ,{
          path: '/products/:id',
          element: <ProductDetails />
        },
        {
          path: '/about',
          element: <div>O nas</div>,
        },
        {
          path: '/login',
          element: <Login />,
        },
        {
          path: '/register',
          element: <Register />
        },
        {
          path: '/cart',
          element: <ShoppingCart />
        },
        {
          path: '/placeorder',
          element: <PlaceOrder />
        },
{
  path: '/placeorderr',
  element: <PlaceOrderR/>
}
        ,
        {
          path: '/admin',
          element: <Admin />,
          children: [
            {
              path: 'orders',
              element: <Orders />,
            },
            {
              path: '/admin/productManagement/orderDetails/:order',
              element: <OrderDetails />
            },
            {
              path: '/admin/productManagement',
              element: <ProductManagement />,
            },
            {
              path: '/admin/productManagement/productEdit/:id',
              element: <ProductEdit />,
            },
            {
              path: '/admin/productManagement/addNewProduct',
              element: <AddNewProduct />,
            },
          ]
        },
      ],
    },
  ]
)

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);