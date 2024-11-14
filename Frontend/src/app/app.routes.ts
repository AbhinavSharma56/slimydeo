import { Routes } from '@angular/router';
import { IndexNavbarComponent } from './components/index-navbar/index-navbar.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

export const routes: Routes = [
    { 
        path: '',  
        component: IndexNavbarComponent,
        children: [
            { 
                path: 'home', 
                component: HomeComponent 
            },
            { 
                path: 'login', 
                component: LoginComponent
            },
            { 
                path: 'register', 
                component: RegisterComponent
            },
            { 
                path: '**', 
                component: PageNotFoundComponent 
            }
        ]
    },
];