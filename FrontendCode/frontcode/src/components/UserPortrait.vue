<template>
  <el-avatar @click="RouteToUserInfo"><user-filled /></el-avatar>
</template>
<script>
import { UserFilled } from "@element-plus/icons";
import router from "@/router/index";
export default {
  name: "UserPortrait",
  components: { UserFilled },
  data() {
    return {
      UserName: "heziang",
    };
  },
  methods: {
    RouteToUserInfo() {
      router.push({ name: "User", params: { userName: this.UserName } });
    },
  },
};
</script>
