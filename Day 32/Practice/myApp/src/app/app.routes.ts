import { Routes } from '@angular/router';
import { First } from './first/first';
import { Login } from './login/login';
import { Products } from './products/products';
import { Home } from './home/home';
import { Profile } from './profile/profile';
import { AuthGuard } from './auth-guard';
import { ProductDetail } from './product-detail/product-detail';

export const routes: Routes = [
   
    {path:'landing',component:First},
    {path:'products',component:Products},
    {path:'login',component:Login},
    // {path:'home/:un',component:Home},

    //  {path:'home',component:Home,children:[
    //     {path:'products',component:Products},
    //     {path:'first',component:First}
    //  ]}
    // { path: '', redirectTo: '/products', pathMatch: 'full' }
    // {path:'home/:un',component:Home,children:
    //     [
    //         {path:'products',component:Products},
    //         {path:'first',component:First}

    //     ]
    // },
    {path:'profile',component:Profile,canActivate:[AuthGuard]},
    {path:'home/:un',component:Home, canActivateChild:[AuthGuard],children:[
        { path: 'first', component: First },
        { path: 'products', component: Products },
        { path: 'products/:id', component: ProductDetail}
    ]}

];
