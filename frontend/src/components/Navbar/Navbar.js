import './Navbar.css';
import { Link, Outlet } from "react-router-dom";
import '../../images/icons/css/fontello.css'
import { useEffect, useState } from 'react';
import { Button } from '@mui/material';
import LogoutIcon from '@mui/icons-material/Logout';
import { Margin } from '@mui/icons-material';
import LoginIcon from '@mui/icons-material/Login';

const Navbar = () => {
    const [user, setUser]=useState({username: "", role: ""});
    const [isLoggedIn, setIsLoggedIn] =useState(true);
    const [isAdmin, setIsAdmin] =useState(false);

    const Logout=()=>{
        localStorage.setItem("token","");
        setIsLoggedIn(false);
        setIsAdmin(false);
    }
    useEffect(() => {
        
        fetch(`${process.env.REACT_APP_API_URL}/api/v1/User/identify`,{
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}` 
            }
        })
      .then((response)=>{
        if(!response.ok)
            throw Error("")
        return response.json()
    })
    .catch(error => {
        
        return {username : "", role: ""};
    })
        .then((data)=>{
            setIsLoggedIn(true);
            console.log(data)
            setUser(data);
            if(data.role=="ADMIN"){
                setIsAdmin(true);
            }
            else{
                setIsAdmin(false);
            }
      });
        },[isLoggedIn])
    
    return ( 
        <div>
            <nav className="nav">
                <div className='navbar'>
                    <Link to="/">Home</Link>
                    <Link to="/products">Products</Link>
                    <Link to="/about">About</Link>
                    {isAdmin && <Link to="/admin/orders">Admin</Link>}
                </div>
                
                    {user.username!="" && 
                <div className='icons'>
                    <div className='icon'>
                    <Link className='cart' to="/cart">
                        <i className="icon-basket"></i>
                    </Link>
                    </div>
                    <div className='icon'>
                        <Link className='cart' to={{pathname:`/user/${user.username}`}}>
                        <i className='icon-user'/>
                        </Link>
                    </div>
                    <div className='icon'>
                            <LogoutIcon  onClick={x=>{Logout()}} color='inherit'sx={{marginTop:"1.5vh"}}/>
                    </div>
               </div>
                    }
                    {user.username==""&&
                    <div className='icons'>
                    <div className='icon'>
                    <Link className='cart' to="/cart">
                        <i className="icon-basket"></i>
                    </Link>
                    </div>
                    <div className='icon' >

                    <Link className='cart' to="/login">
                        <LoginIcon/>
                    </Link>
                </div>
                </div>
                    }
                   
            </nav>
            <div className="wrapper">
                <Outlet />
            </div>
          
            </div>
     );
}
 
export default Navbar;