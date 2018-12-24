// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  azureConfig: {
    //tenant: 'shreedharpatil072gmail.onmicrosoft.com',
    tenant: 'ca07c276-28d9-4b09-86e1-6fadd84de79f',
    clientId: '75a483d2-a35f-4fe6-8c07-fa48ef6bc5b7',
    //resource: 'fdb6b9cb-3378-4bc0-b08f-3e0377af9eff',
    postLogoutRedirectUri: 'https://advancedenergymeter.azurewebsites.net/',
    redirectUri: 'https://advancedenergymeter.azurewebsites.net/viewusers'
    //postLogoutRedirectUri: 'http://localhost:65148',
    //redirectUri: 'http://localhost:65148/viewusers'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
