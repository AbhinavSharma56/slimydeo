import { Routes } from '@angular/router';
import { IndexNavbarComponent } from './components/index-navbar/index-navbar.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UserNavbarComponent } from './components/user components/user-navbar/user-navbar.component';
import { authGuard } from './guards/auth.guard';
import { AdminNavbarComponent } from './components/admin components/admin-dashboard/admin-navbar.component';
import { loginGuard } from './guards/login.guard';
import { UserDashboardComponent } from './components/user components/user-dashboard/user-dashboard.component';
import { ExerciseDashboardComponent } from './components/user components/exercise-dashboard/exercise-dashboard.component';
import { HealthDashboardComponent } from './components/user components/health-dashboard/health-dashboard.component';
import { MealDashboardComponent } from './components/user components/meal-dashboard/meal-dashboard.component';
import { MoodDashboardComponent } from './components/user components/mood-dashboard/mood-dashboard.component';
import { UserProfileComponent } from './components/user components/user-profile/user-profile.component';

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
                component: LoginComponent,
                canActivate: [loginGuard]
            },
            { 
                path: 'register', 
                component: RegisterComponent
            },
            {
                path: '',  
                redirectTo: 'home',  
                pathMatch: 'full'
            },
        ]
    },
    {
        path: 'user',
        component: UserNavbarComponent,
        children: [
            {
                path: 'dashboard',
                component: UserDashboardComponent,
            },
            {
                path: 'health',
                component: HealthDashboardComponent,
            },
            {
                path: 'exercise',
                component: ExerciseDashboardComponent,
            },
            {
                path: 'meal',
                component: MealDashboardComponent,
            },
            {
                path: 'mood',
                component: MoodDashboardComponent,
            },
            {
                path: 'profile',
                component: UserProfileComponent,
            },
            {
                path: '',  
                redirectTo: 'dashboard',  
                pathMatch: 'full'
            },
        ],
        canActivate: [authGuard]
    },
    {
        path: 'admin',
        component: AdminNavbarComponent ,
        children: [
        ],
        canActivate: [authGuard]
    },
    { 
        path: '**', 
        component: PageNotFoundComponent 
    }
];