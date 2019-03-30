import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/./nav-menu/nav-menu.component';
import { HomeComponent } from './Components/./home/home.component';
import { CounterComponent } from './Components/./counter/counter.component';
import { FetchDataComponent } from './Components/./fetch-data/fetch-data.component';
import { LoginComponent } from './Components/./login/login.component';
import { RegisterComponent } from './Components/./register/register.component';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { AuthGuardService } from './auth-guard.service';
import { ExpressService } from './express.service';
import { GeneralService } from './general.service';
import { MyInterceptor } from './interceptor';
import { ProfileComponent } from './Components/./profile/profile.component';
import { KnowledgesComponent } from './Components/knowledges/knowledges.component';
import { SocialLoginModule, AuthServiceConfig } from "angular5-social-login";
import { FacebookLoginProvider } from "angular5-social-login";
import { getAuthServiceConfigs } from "./socialloginConfig";
import { NavMenuTopComponent } from './Components/nav-menu-top/nav-menu-top.component';
import { JobComponent } from './Components/job/job.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    KnowledgesComponent,
    NavMenuTopComponent,
    JobComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AngularFontAwesomeModule,
    ReactiveFormsModule,
    SocialLoginModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuardService] },
      { path: 'job', component: JobComponent, canActivate: [AuthGuardService] }
    ])
  ],
  providers: [AuthGuardService, ExpressService, {
    provide: HTTP_INTERCEPTORS,
    useFactory: getAuthServiceConfigs,
    useClass: MyInterceptor,
    multi: true
  },
    {
      provide:  AuthServiceConfig,
      useFactory: getAuthServiceConfigs
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
