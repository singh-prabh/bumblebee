const users = require('./users/users.service.js');
const person = require('./person/person.service.js');
const company = require('./company/company.service.js');
const states = require('./states/states.service.js');
const address = require('./address/address.service.js');
const phone = require('./phone/phone.service.js');
module.exports = function (app) {
  app.configure(users);
  app.configure(person);
  app.configure(company);
  app.configure(states);
  app.configure(address);
  app.configure(phone);
};
