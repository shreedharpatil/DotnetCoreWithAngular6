import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomepageComponent } from './homepage/homepage.component';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { AddStateComponent } from './add-state/add-state.component';
import { AddDistrictComponent } from './add-district/add-district.component';
import { AddTalukComponent } from './add-taluk/add-taluk.component';
import { AddVillageComponent } from './add-village/add-village.component';
import { ViewFeederComponent } from './view-feeder/view-feeder.component';
import { LoadsheddingInfoComponent } from './loadshedding-info/loadshedding-info.component';
import { AdalService } from 'adal-angular4';
import { AuthGuard } from './AuthGuard';
import { HttpConfigInterceptor } from './interceptor/HttpConfigInterceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    LoginComponent,
    CreateUserComponent,
    ViewUsersComponent,
    AddStateComponent,
    AddDistrictComponent,
    AddTalukComponent,
    AddVillageComponent,
    ViewFeederComponent,
    LoadsheddingInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule
  ],
  providers: [AuthGuard, AdalService, 
    { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
