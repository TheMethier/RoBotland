import './Navbar.css';
import { Link, Outlet } from "react-router-dom";
import '../../images/icons/css/fontello.css'

const isAdmin = true;

const Navbar = () => {

    return ( 
        <div>
            <nav className="nav">
                <div className='navbar'>
                    <Link to="/">Home</Link>
                    <Link to="/products">Products</Link>
                    <Link to="/about">About</Link>
                    {isAdmin && <Link to="/admin/orders">Admin</Link>}
                </div>
                <div className='icons'>
                <div className='icon'>
                    <Link className='cart' to="/cart">
                        <i className="icon-basket"></i>
                    </Link>
                </div>
                <div className='icon'>
                    <Link className='cart' to="/cart">
                        <i className="icon-user"></i>
                    </Link>
                </div>
                </div>
            </nav>
            <div className="wrapper">
                <Outlet />
            </div>
          
            </div>
     );
}
 
export default Navbar;