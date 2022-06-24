<template>
  <el-aside>
    <UserPortrait />
    <p class="hotword">热词榜</p>
    <el-menu>
      <el-sub-menu index="1">
        <template #title>
          <el-icon><Document /></el-icon>{{ HotWordsOfMeeting[0].meeting }}
        </template>
        <el-menu-item-group>
          <el-menu-item
            v-for="hotword in HotWordsOfIccv"
            :key="hotword.rank"
            @click="ShowHotWordEssays(HotWordsOfMeeting[0].meeting, $event)"
          >
            <!--展示对应热词的相关论文 -->
            {{ hotword.word }}</el-menu-item
          >
        </el-menu-item-group>
      </el-sub-menu>
      <el-sub-menu index="2">
        <template #title>
          <el-icon><Document /></el-icon>{{ HotWordsOfMeeting[1].meeting }}
        </template>
        <el-menu-item-group>
          <el-menu-item
            v-for="hotword in HotWordsOfCvpr"
            :key="hotword.rank"
            index="2-"
            +
            hotwords.rank
            @click="ShowWordEssay(HotWordsOfMeeting[1].meeting, $event)"
          >
            <!--展示对应热词的相关论文 -->
            {{ hotword.word }}</el-menu-item
          >
        </el-menu-item-group>
      </el-sub-menu>
      <el-sub-menu index="3">
        <template #title>
          <el-icon><Document /></el-icon>{{ HotWordsOfMeeting[2].meeting }}
        </template>
        <el-menu-item-group>
          <el-menu-item
            v-for="hotword in HotWordsOfEccv"
            :key="hotword.rank"
            index="3-"
            +
            hotword.rank
            @click="ShowWordEssay(HotWordsOfMeeting[2].meeting, $event)"
          >
            <!--展示对应热词的相关论文 -->
            {{ hotword.word }}</el-menu-item
          >
        </el-menu-item-group>
      </el-sub-menu>
    </el-menu>
  </el-aside>
</template>
<script>
import UserPortrait from "./UserPortrait.vue";
import { Document } from "@element-plus/icons";
import store from "@/store/";
import { getHotword } from "@/axios/axios";
export default {
  name: "AsidePart1",
  components: { UserPortrait, Document },
  data() {
    return {
      HotWordsOfMeeting: [],
    };
  },
  mounted() {
    getHotword().then((res) => {
      this.HotWordsOfMeeting = res.data;
    });
  },
  methods: {
    ShowWordEssay(meeting, event) {
      var hotword = {
        meeting: meeting,
        word: event.target.innertext,
      };
      store.commit("CovertSort", hotword);
    },
  },
};
</script>
