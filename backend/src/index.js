// const crypto = require('crypto')

const server = require('./server')

const PORT = 443
server.listen(PORT, () => {
  console.log(`Listening on http://localhost:${PORT}`)
})
