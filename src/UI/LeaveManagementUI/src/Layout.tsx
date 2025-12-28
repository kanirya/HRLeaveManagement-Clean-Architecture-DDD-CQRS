
import {Link, Outlet} from "react-router-dom";
function Layout() {
    return (
        <>
            <div className="Header sticky z-50 top-0">
                <h2 className="text-gray-100">Leave Management</h2>
                <Link className="Button rounded-xl bg-gray-100 px-2 py-1 hover:bg-light-200 " to="/login">
                    Login
                </Link>
            </div>
        <Outlet />


        </>

    )
}

export default Layout
