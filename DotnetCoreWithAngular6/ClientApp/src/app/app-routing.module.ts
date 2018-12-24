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
import { LoadsheddingInfoComponent } from './loadshedding-info/loadshedding-info.component';
import { AuthGuard } from './AuthGuard';


const routes: Routes = [
  {
    path: '', component: LoginComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'homepage', component: HomepageComponent, canActivate: [AuthGuard]
  },
  {
    path: 'createuser', component: CreateUserComponent, canActivate: [AuthGuard]
  },
  {
    path: 'viewusers', component: ViewUsersComponent, canActivate: [AuthGuard]
  },
  {
    path: 'addstate', component: AddStateComponent, canActivate: [AuthGuard]
  },
  {
    path: 'adddistrict', component: AddDistrictComponent, canActivate: [AuthGuard]
  },
  {
    path: 'addtaluk', component: AddTalukComponent, canActivate: [AuthGuard]
  },
  {
    path: 'addvillage', component: AddVillageComponent, canActivate: [AuthGuard]
  },
  {
    path: 'viewfeeders', component: ViewFeederComponent, canActivate: [AuthGuard]
  },
  {
    path: 'loadsheddingmessage', component: LoadsheddingInfoComponent, canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
