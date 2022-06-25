<template>
  <el-avatar @click="RouteToUserInfo"><user-filled /></el-avatar>
  <p>账户</p>
  <el-input class="log" v-model="account" />
  <p>密码</p>
  <el-input class="log" v-model="password" />
  <br />
  <p>请选择你感兴趣的类别</p>
  <div>
    <el-checkbox
      v-model="fieldInterested[0]"
      label="Computer vision and Pattern recognition"
      size="large"
      border
    />
    <el-checkbox
      v-model="fieldInterested[1]"
      label="Machine Learning"
      size="large"
      border
    />
    <el-checkbox
      v-model="fieldInterested[2]"
      label="Robotics"
      size="large"
      border
    />
    <el-checkbox
      v-model="fieldInterested[3]"
      label="Image and Video processing"
      size="large"
      border
    />
    <el-checkbox
      v-model="fieldInterested[4]"
      label="Artificial Intelligence"
      size="large"
      border
    />
    <el-checkbox
      v-model="fieldInterested[5]"
      label="Others"
      size="large"
      border
    />
  </div>
  <br />
  <el-button type="success" @click="Register">注册</el-button>
</template>
<script>
import { UserFilled } from "@element-plus/icons";
//import { register } from "@/axios/axios";
export default {
  name: "loadPage",
  components: { UserFilled },
  data() {
    return {
      account: "",
      password: "",
      fieldInterested: [false, false, false, false, false, false],
    };
  },
  methods: {
    Register() {
      //register();
    },
  },
};
</script>
<style scoped>
.log {
  width: 400px;
}
.el-button {
  margin: 50px;
}
.el-avatar {
  margin-top: 150px;
}
</style>
