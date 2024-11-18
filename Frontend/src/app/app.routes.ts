import { Routes } from '@angular/router';
import { IndexNavbarComponent } from './components/index-navbar/index-navbar.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UserNavbarComponent } from './components/user components/user-navbar/user-navbar.component';
import { authGuard } from './guards/auth.guard';
import { AdminNavbarComponent } from './components/admin components/admin-navbar/admin-navbar.component';
import { loginGuard } from './guards/login.guard';
import { UserDashboardComponent } from './components/user components/user-dashboard/user-dashboard.component';
import { ExerciseDashboardComponent } from './components/user components/exercise-dashboard/exercise-dashboard.component';
import { HealthDashboardComponent } from './components/user components/health-dashboard/health-dashboard.component';
import { MealDashboardComponent } from './components/user components/meal-dashboard/meal-dashboard.component';
import { MoodDashboardComponent } from './components/user components/mood-dashboard/mood-dashboard.component';
import { UserProfileComponent } from './components/user components/user-profile/user-profile.component';
import { ListMealComponent } from './components/user components/meal-dashboard/list-meal/list-meal.component';
import { AddMealComponent } from './components/user components/meal-dashboard/add-meal/add-meal.component';
import { UpdateMealComponent } from './components/user components/meal-dashboard/update-meal/update-meal.component';
import { DeleteMealComponent } from './components/user components/meal-dashboard/delete-meal/delete-meal.component';
import { ExerciseTypeComponent } from './components/admin components/exercise-type/exercise-type.component';
import { MoodTypeComponent } from './components/admin components/mood-type/mood-type.component';
import { UserDataComponent } from './components/admin components/user-data/user-data.component';
import { MetricTypeComponent } from './components/admin components/metric-type/metric-type.component';
import { AboutComponent } from './components/about/about.component';

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
                path: 'about',
                component: AboutComponent
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
                children: [
                    {
                        path: '',
                        component: ListMealComponent,
                        children: [
                            {
                                path: 'add',
                                component: AddMealComponent
                            },
                            {
                                path: 'update',
                                component: UpdateMealComponent
                            },
                            {
                                path: 'delete',
                                component: DeleteMealComponent
                            }
                        ]
                    }
                ]
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
            {
                path: 'metric-type',
                component: MetricTypeComponent
            },
            {
                path: 'exercise-type',
                component: ExerciseTypeComponent
            },
            {
                path: 'mood-type',
                component: MoodTypeComponent
            },
            {
                path: 'user-data',
                component: UserDataComponent
            },
            {
                path: '',  
                redirectTo: 'user-data',  
                pathMatch: 'full'
            }
        ],
        canActivate: [authGuard]
    },
    { 
        path: '**', 
        component: PageNotFoundComponent 
    }
];