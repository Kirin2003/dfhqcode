<template>
  <el-avatar @click="RouteToUserInfo"><user-filled /></el-avatar>
  <p>账户</p>
  <el-input class="log" v-model="account" />
  <p>密码</p>
  <el-input class="log" v-model="password" />
  <br />
  <el-button type="primary" @click="Login">登录</el-button>
  <el-button type="success" @click="Register">注册</el-button>
</template>
<script>
import { UserFilled } from "@element-plus/icons";
import router from "@/router/index";
import { login } from "@/axios/axios";
import store from "@/store/index";
export default {
  name: "loadPage",
  components: { UserFilled },
  data() {
    return {
      account: "",
      password: "",
    };
  },
  methods: {
    Login() {
      login(this.account, this.password).then((res) => {
        var user = res.data.userId;
        store.commit("ConvertUserId", user);
      });
      router.push({ name: "home" });
    },
    Register() {
      router.push({ name: "Register" });
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
