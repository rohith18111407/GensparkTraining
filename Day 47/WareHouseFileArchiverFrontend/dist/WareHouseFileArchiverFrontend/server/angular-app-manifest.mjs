
export default {
  bootstrap: () => import('./main.server.mjs').then(m => m.default),
  inlineCriticalCss: true,
  baseHref: '/',
  locale: undefined,
  routes: [
  {
    "renderMode": 2,
    "redirectTo": "/login",
    "route": "/"
  },
  {
    "renderMode": 2,
    "route": "/login"
  },
  {
    "renderMode": 2,
    "route": "/signup"
  },
  {
    "renderMode": 2,
    "route": "/admin/dashboard"
  },
  {
    "renderMode": 2,
    "route": "/staff/dashboard"
  }
],
  entryPointToBrowserMapping: undefined,
  assets: {
    'index.csr.html': {size: 5115, hash: '587c7cf3e7fe14d6553b4e0f54b7b07df8b029cad81a4b7d7f89d26f9a8a15f5', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 1026, hash: 'f334b723c6d4e2ad4b188683af834d3bf8b90adcc1b822293ca37b7789fbbfc8', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'login/index.html': {size: 23462, hash: 'b0da558711b97c4a77042c13d554d8aab463d88cb44feffcb84f8c8f2b251d6a', text: () => import('./assets-chunks/login_index_html.mjs').then(m => m.default)},
    'admin/dashboard/index.html': {size: 22069, hash: '3072e46fab729a4a3868ff58340b1bb597af63217d65dfab589a3b0b8f991c37', text: () => import('./assets-chunks/admin_dashboard_index_html.mjs').then(m => m.default)},
    'signup/index.html': {size: 23660, hash: 'efcead821730743096ac80873d863ab99782c6283bdea179c6b1092974dfed75', text: () => import('./assets-chunks/signup_index_html.mjs').then(m => m.default)},
    'staff/dashboard/index.html': {size: 22040, hash: 'fac044ee0cc9522e897c3f2b9e3b01559c545315ccd0481d1d640999bfd6777f', text: () => import('./assets-chunks/staff_dashboard_index_html.mjs').then(m => m.default)},
    'styles-AI67RPST.css': {size: 317106, hash: 'aUsARTr9Spg', text: () => import('./assets-chunks/styles-AI67RPST_css.mjs').then(m => m.default)}
  },
};
