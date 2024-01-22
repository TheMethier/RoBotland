import './Admin.css';
import { Link, Outlet } from "react-router-dom";
import '../../images/icons/css/fontello.css'

const Admin = () => {

    return ( 
        <div>
            <nav className="nav">
                <div className='navbar'>
                    <Link to="orders">Zamówienia</Link>
                    <Link to="/admin/productManagement/addNewProduct">Dodaj nowy produkt</Link>
                    <Link to="productManagement">Zarządzaj produktami</Link>
                </div>
            </nav>
            <div className="wrapper">
                <Outlet />
            </div>     
        </div>
     );
}
 
export default Admin;