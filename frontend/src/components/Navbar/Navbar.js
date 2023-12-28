import './Navbar.css';
import { Link, Outlet } from "react-router-dom";

const Navbar = () => {
    return ( 
        <div>
            <nav className="navbar">
                <Link to="/">Home</Link>
                <Link to="/products">Products</Link>
                <Link to="/about">About</Link>
            </nav>
            <div className="wrapper">
                <Outlet />
            </div>
        </div>
     );
}
 
export default Navbar;