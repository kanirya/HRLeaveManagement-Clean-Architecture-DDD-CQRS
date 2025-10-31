import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'

import {createBrowserRouter, RouterProvider} from 'react-router-dom'
import Layout from "./Layout.tsx"
import {Login} from "./features/auth/pages/Login.tsx";
import Register from "./features/auth/pages/Register.tsx";
import LandingPage from "./features/admin/pages/LandingPage.tsx";
import Page from "./features/admin/pages/LandingPage.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Page />,
        // errorElement: <GlobalErrorPage />,
        children: [
            { index: true, element: <Page /> },
            { path: "/login", element: <Login /> },
            { path: "/register", element: <Register /> },
            // {
            //     element: <PrivateRoute roles={["Admin"]} />,
            //     children: [{ path: "/dashboard/:id?", element: <Dashboard /> }],
            // },
            // {
            //     element: <PrivateRoute  />,
            //     children: [{path:"/:id", element: <Profile /> }],
            // }
        ],
    },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>

          <RouterProvider router={router} />


  </StrictMode>,
)
