import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { AddStateComponent } from './add-state/add-state.component';
import { AddDistrictComponent } from './add-district/add-district.component';

const routes: Routes = [
  {
    path: '', component: LoginComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'homepage', component: HomepageComponent
  },
  {
    path: 'createuser', component: CreateUserComponent
  },
  {
    path: 'viewusers', component: ViewUsersComponent
  },
  {
    path: 'addstate', component: AddStateComponent
  },
  {
    path: 'adddistrict', component: AddDistrictComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
