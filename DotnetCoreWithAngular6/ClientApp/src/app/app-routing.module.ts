import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { AddStateComponent } from './add-state/add-state.component';
import { AddDistrictComponent } from './add-district/add-district.component';
import { AddTalukComponent } from './add-taluk/add-taluk.component';
import { AddVillageComponent } from './add-village/add-village.component';
import { ViewFeederComponent } from './view-feeder/view-feeder.component';


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
  },
  {
    path: 'addtaluk', component: AddTalukComponent
  },
  {
    path: 'addvillage', component: AddVillageComponent
  },
  {
    path: 'viewfeeders', component: ViewFeederComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
