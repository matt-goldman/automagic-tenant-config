import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {LottieAnimationViewModule} from 'ng-lottie';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './header/header.component';
import { MatButtonModule,
         MatCardModule,
         MatDialogModule,
         MatMenuModule,
         MatToolbarModule,
         MatIconModule,
         MatSidenavModule,
         MatFormFieldModule,
         MatInputModule,
         MatListModule,
         MatSnackBarModule,
         MatTableModule,
         MatDatepickerModule,
         MatNativeDateModule,
         MatAutocompleteModule} from '@angular/material';
import { PatientsComponent } from './patients/patients.component';
import { MedicationsComponent } from './medications/medications.component';
import { PrescriptionsComponent } from './prescriptions/prescriptions.component';
import { AdministrationsComponent } from './administrations/administrations.component';
import { routing } from './app.routing';
import { IdentityModule } from './identity/identity.module';
import { HomeComponent } from './home/home.component';
import { UserService } from './services/user.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PatientsClient, MedicationsClient, PrescriptionsClient, AdministrationsClient, API_BASE_URL } from '../helpers/api-client';
import { TokenInterceptor } from './httpinterceptor';
import { AddPatientsComponent } from './add-patients/add-patients.component';
import { MedListComponent } from './med-list/med-list.component';
import { AddMedsDialogComponent } from './add-meds-dialog/add-meds-dialog.component';
import { AddPrescriptionComponent } from './add-prescription/add-prescription.component';
import { AddAdministrationComponent } from './add-administration/add-administration.component';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { UnauthComponent } from './unauth/unauth.component';
import { MsalModule } from '@azure/msal-angular'
import * as env from '../environments/environment';
import { QrDialogComponent } from './qr-dialog/qr-dialog.component';
import { QRCodeModule } from 'angularx-qrcode';

export function JwtTokenGetter() {
  return localStorage.getItem("auth_token");
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    PatientsComponent,
    MedicationsComponent,
    PrescriptionsComponent,
    AdministrationsComponent,
    HomeComponent,
    AddPatientsComponent,
    MedListComponent,
    AddMedsDialogComponent,
    AddPrescriptionComponent,
    AddAdministrationComponent,
    UnauthComponent,
    QrDialogComponent
  ],
  entryComponents: [
    AddMedsDialogComponent,
    QrDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatDatepickerModule,
    MatDialogModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatSnackBarModule,
    MatListModule,
    MatFormFieldModule,
    MatTableModule,
    BrowserAnimationsModule,
    routing,
    IdentityModule,
    LottieAnimationViewModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: JwtTokenGetter
      }
    }),
    MsalModule.forRoot({
      clientID: env.B2CClientId,
      authority: `https://${env.B2CTenant}.b2clogin.com/${env.B2CTenant}.onmicrosoft.com/${env.SigninPolicy}`,
      redirectUri: env.RedirectUri,
      validateAuthority : false,
      cacheLocation : "localStorage",
      storeAuthStateInCookie: false, // dynamically set to true when IE11
      postLogoutRedirectUri: env.RedirectUri,
      navigateToLoginRequestUrl : true,
      popUp: true,
      consentScopes: ["user_impersonation"],
      unprotectedResources: ["https://angularjs.org/"],
      correlationId: '1234'
    }),
    QRCodeModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: API_BASE_URL,
      useValue: env.ApiBaseUri
    },
    UserService,
    PatientsClient,
    MedicationsClient,
    PrescriptionsClient,
    AdministrationsClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
