const { createServer } = require('http')
const WebSocket = require('ws')
const app = require('./app')
const HashMap = require('hashmap')

const server = createServer(app)
const wss = new WebSocket.Server({ server })

let socket_hashmap = new HashMap()

const max_client_count = 3
const random_motion_count = 3
const random_motion_start = 20
const random_motion_during = 11
const game_start_countdown = 3
const die_count = 3
const npc_count = 40

let count = 0 // 1초동안 들어온 소켓의 개수 저장.
let client_count = 0 // 서버에 들어온 소켓의 수 저장.
let client_id = 0
let user_info = {} // 유저 소켓을 저장할 객체. { id: ws value }
let received_strings = [] // 유저들이 소켓을 보낼 때, 들어온 문자열들을 저장할 배열. 나중에 join 메소드를 통해 슬래시(/)로 연결할 예정.
let flag_info = {}

/*
const toBytes = (str) => {
  const buffer = Buffer.from(str, 'utf8')
  const result = Array(buffer.length)
  for (let i = 0; i < buffer.length; ++i) {
    result[i] = buffer[i]
  }
  return result
}
*/

const randomMotion = (time, id) => {
  console.log(time, id)
  sendBroadcastMessage('random_move', time + '_' + id)
}

const sendBroadcastMessage = (eventName, message) => {
  const sendMessage = eventName + '!' + message
  socket_hashmap.forEach((value, key) => {
    value.send(sendMessage)
  })
}

const allUserSendPosition = () => {
  const send_string = received_strings.join('/')
  // console.log(send_string)

  // const bytes_string = toBytes(send_string)
  sendBroadcastMessage('all_position', send_string)
  received_strings = []
  user_info = {}
}

const parseStringAndSendData = (str) => {
  const splittedString = str.split('!')
  // console.log(splittedString)
  if (splittedString[0] === 'unit_position') {
    const id = splittedString[1].charAt(0)

    if (user_info[id] === undefined) {
      user_info[id] = splittedString[1][0]
      const splitted_info = splittedString[1].split(';')
      received_strings = received_strings.concat(splittedString[1])

      // flag 초기화
      for (let i = 0; i < splitted_info.length; ++i) {
        const splitted_unit = splitted_info[i].split(',')
        if (flag_info[splitted_unit[0]] === undefined) {
          flag_info[splitted_unit[0]] = 0
        }
      }

      if (Object.keys(user_info).length === client_count) {
        allUserSendPosition()
      }
    }
  } else if (splittedString[0] === 'hit') {
    const unit_id = splittedString[1]
    console.log(unit_id)

    if (unit_id === 'Wall(Clone)' || unit_id === 'WallTileMap') return

    ++flag_info[unit_id]
    console.log(flag_info[unit_id] + ' | ' + unit_id.toString())
    if (flag_info[unit_id] >= die_count) {
      sendBroadcastMessage('die', unit_id.toString())
    }
  } else if (splittedString[0] === 'zoomout') {
    console.log('zoom out is completed')
    const time =
      parseInt(Math.random() * random_motion_during) + random_motion_start
    const random_motion_id = parseInt(
      Math.random() * random_motion_count
    ).toString()
    randomMotion(time, random_motion_id)
    // setTimeout(() => randomMotion(time), time * 1000)
  } else {
    console.log(splittedString[0])
  }
}

wss.on('connection', (ws) => {
  console.log('client joined.')
  const socket_id = client_id++
  client_count++
  socket_hashmap.set(socket_id, ws) // socket id를 hashmap에 저장

  if (client_count > max_client_count) {
    --client_count
    return
  }

  flag_info[socket_id.toString()] = 0
  for (let i = 0; i < npc_count; ++i) {
    const npc_id = socket_id + '_' + i
    flag_info[npc_id] = 0
  }

  ws.send(`id_set!${socket_id}`) // 유저에게 socket id 전달 -> 앞으로 들어올 문자열 제일 앞에 들어올 예정
  console.log(socket_id)

  if (client_count === max_client_count) {
    sendBroadcastMessage('count_down', '.')
    const time =
      parseInt(Math.random() * random_motion_during) + random_motion_start
    const random_motion_id = parseInt(
      Math.random() * random_motion_count
    ).toString()
    setTimeout(
      () => randomMotion(time, random_motion_id),
      (time + game_start_countdown) * 1000
    )
  }

  ws.on('message', (data) => {
    ++count
    // console.log(data) --> raw data print

    // if data type is string
    if (typeof data === 'string') {
      // console.log("string received from client -> '" + data + "'")
      parseStringAndSendData(data)
    }

    // if data type is bytes array
    else {
      const string_data = Buffer.from(data).toString('utf8')
      parseStringAndSendData(string_data)
    }
  })

  // count 출력
  /*
  setInterval(() => {
    console.log(count)
    count = 0
  }, 1000)
  */

  ws.on('close', () => {
    const closedKey = socket_hashmap.search(ws)
    client_count--
    console.log(closedKey)
    socket_hashmap.delete(closedKey)
    console.log('client left.')
  })
})

module.exports = server
